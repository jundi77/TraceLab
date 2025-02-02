// TraceLab - Software Traceability Instrument to Facilitate and Empower Traceability Research
// Copyright (C) 2012-2013 CoEST - National Science Foundation MRI-R2 Grant # CNS: 0959924
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see<http://www.gnu.org/licenses/>.

using System;
using TraceLab.Core.ViewModels;
using MonoDevelop.Components.Docking;
using Mono.Unix;
using TraceLab.Core.Workspaces;
using System.Collections.Specialized;
using Gtk;
using System.Collections.Generic;
using System.Linq;

namespace TraceLab.UI.GTK
{
    public class WorkspaceWindowPad : IDockPad
    {
        static WorkspaceWindowPad() 
        {
            s_workspaceViewerIcon = Gdk.Pixbuf.LoadFromResource("TraceLab.UI.GTK.Resources.workspaceViewer.png");
            s_noViewerIcon = Gdk.Pixbuf.LoadFromResource("TraceLab.UI.GTK.Resources.noViewer.png");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceLab.UI.GTK.WorkspaceWindowPad"/> class.
        /// </summary>
        public WorkspaceWindowPad()
        {
        }

        /// <summary>
        /// Initialize the window pad in the given dock frame.
        /// </summary>
        /// <param name='dockFrame'>
        /// Dock frame.
        /// </param>
        public void Initialize(DockFrame dockFrame)
        {
            m_dockFrame = dockFrame;
            DockItem workspaceDockingWindow = m_dockFrame.AddItem("Workspace");
            workspaceDockingWindow.Label = Catalog.GetString("Workspace");
            workspaceDockingWindow.DefaultLocation = "ComponentsLibrary/Center";
            workspaceDockingWindow.Behavior |= DockItemBehavior.CantClose;

            workspaceDockingWindow.Content = CreateWorkspaceView();

            m_initialized = true;
        }

        /// <summary>
        /// Sets the application model on the given pad.
        /// Pad refreshes its information according to the given application model.
        /// </summary>
        /// <param name='applicationViewModel'>
        /// Application model.
        /// </param>
        public void SetApplicationModel(ApplicationViewModel applicationViewModel) 
        {
            if(m_initialized == false || m_dockFrame.GdkWindow == null) 
            {
                //GdkWindow is for each dock frame is assigned when windowShell calls ShowAll(). See DockContainer.OnRealize method
                throw new InvalidOperationException("WorkspaceWindowPad must be first initialized and dockFrame must have assigned GdkWindow before setting application model.");
            }

            if(applicationViewModel.WorkspaceViewModel != null) 
            {
                applicationViewModel.PropertyChanged += HandlePropertyChanged;

                SetWorkspaceViewModel(applicationViewModel);
            }
        }

        private void HandlePropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ApplicationViewModel applicationViewModel = sender as ApplicationViewModel;
            if(applicationViewModel != null && e.PropertyName == "WorkspaceViewModel") 
            {
                SetWorkspaceViewModel(applicationViewModel);
            }
        }

        private void SetWorkspaceViewModel(ApplicationViewModel applicationViewModel) 
        {
            if(m_workspaceViewModel != null) 
            {
                //detach handlers
                ((INotifyCollectionChanged)m_workspaceViewModel.WorkspaceUnitCollection).CollectionChanged -= WorkspaceCollectionChanged;
                m_workspaceStore.Clear();
            }
            
            m_workspaceViewModel = applicationViewModel.WorkspaceViewModel;
            m_experimentId = applicationViewModel.Experiment.ExperimentInfo.Id;
            
            // Load existing tasks
            AddUnits(m_workspaceViewModel.WorkspaceUnitCollection);

            //attach listener to workspace collection
            ((INotifyCollectionChanged)m_workspaceViewModel.WorkspaceUnitCollection).CollectionChanged += WorkspaceCollectionChanged;
        }

        private Gtk.Widget CreateWorkspaceView() 
        {
            ScrolledWindow sw = new ScrolledWindow();
            m_treeView = new Gtk.TreeView();

            //first column has workspaceUnit but presents its friendly name, 2nd column displays type and 3rd displays value
            m_workspaceStore = new Gtk.ListStore(typeof(WorkspaceUnit), typeof(WorkspaceUnit), typeof(WorkspaceUnit));
            m_treeView.Model = m_workspaceStore;

            //create columns with associated cell renderings
            CellRendererText nameRenderer = new CellRendererText();
            TreeViewColumn nameColumn = m_treeView.AppendColumn("Name", nameRenderer);
            nameColumn.SetCellDataFunc(nameRenderer, new TreeCellDataFunc(RenderName));            

            CellRendererText typeRenderer = new CellRendererText();
            TreeViewColumn typeColumn = m_treeView.AppendColumn("Type", typeRenderer);
            typeColumn.SetCellDataFunc(typeRenderer, new TreeCellDataFunc(RenderType));            

            TreeViewColumn valueColumn = new TreeViewColumn();
            valueColumn.Title = "Value";
            CellRendererPixbuf valueViewerIconCellRenderer = new CellRendererPixbuf();
            CellRendererText valueTextRenderer = new CellRendererText();
            valueViewerIconCellRenderer.Xalign = 0.0f;
            valueColumn.PackEnd(valueTextRenderer, true);
            valueColumn.PackStart(valueViewerIconCellRenderer, false);
            valueColumn.SetCellDataFunc(valueViewerIconCellRenderer, new TreeCellDataFunc(RenderViewerIcon));
            valueColumn.SetCellDataFunc(valueTextRenderer, new TreeCellDataFunc(RenderValue));
            m_treeView.AppendColumn(valueColumn);

            m_treeView.RowActivated += HandleRowActivated;

            // HERZUM SPRINT 3: TLAB-86
            VBox vbox1 = new VBox();
            sw.Add(m_treeView);
            vbox1.Add (sw);
            Box.BoxChild w1 = ((Box.BoxChild)(vbox1 [sw]));
            w1.Position = 0;

            baseline.CanFocus = false;

            Box.BoxChild w2 = ((Box.BoxChild)(vbox1 [baseline]));
            vbox1.Add (baseline);
            w2.Position = 2;
            w2.Expand = false;
            w2.Fill = false;


            vbox1.ShowAll ();
            return vbox1;

            //sw.Add(m_treeView);
            //sw.ShowAll();
            //return sw;

            // END HERZUM SPRINT 3: TLAB-86
        }

        // HERZUM SPRINT 3: TLAB-86
        TextView baseline = new TextView();
        public void ShowBaseline (string type, string value)
        {
            baseline.Buffer.Text = "BASELINE" + "  " + type + "   " + value;
        }
        // END HERZUM SPRINT 3: TLAB-86

        private void HandleRowActivated (object source, RowActivatedArgs args)
        {
            TreeIter item;
            if(m_treeView.Selection.GetSelected(out item)) 
            {
                WorkspaceUnit unit = (WorkspaceUnit)m_treeView.Model.GetValue(item, 0);

                //prepare display funtions
                //first try to display it with GTK, and eventually fallback to windows form display
                var displaysFuncs = new WorkspaceViewerLoader.DisplayEditor[]
                {
                    DisplayGTKWindow,
                    WorkspaceViewerLoader.DisplayWindowsFormEditor
                };

                String error;
                if(!WorkspaceViewerLoader.LoadViewer(unit.Data, unit.FriendlyUnitName, 
                                                     WorkspaceUIAssemblyExtensions.Extensions, displaysFuncs, 
                                                     out error))
                {
                    NLog.LogManager.GetCurrentClassLogger().Warn(error);
                }
            }
        }

        /// <summary>
        /// Displays the GTK window.
        /// </summary>
        /// <param name="editor">The editor.</param>
        /// <param name="windowTitle">The window title.</param>
        /// <returns>if window has been displayed, otherwise false</returns>
        private bool DisplayGTKWindow(TraceLabSDK.IWorkspaceUnitEditor editor, String windowTitle)
        {
            DockItem dockingWindow = m_dockFrame.GetItem(windowTitle);
            if(dockingWindow == null) 
            {
                dockingWindow = m_dockFrame.AddItem(windowTitle);
                dockingWindow.DefaultLocation = "Workspace/Bottom";
                dockingWindow.Behavior = DockItemBehavior.Normal;

                dockingWindow.Visible = true;

                //Float window
                m_dockFrame.SetStatus(dockingWindow, DockItemStatus.Floating);
                Gdk.Rectangle floatRectangle = dockingWindow.FloatingPosition;
                floatRectangle.Width = 350;
                floatRectangle.Height = 180;

                //location of info box next to the component node just sligthly below cursor click
                floatRectangle.X = 150;
                floatRectangle.Y = 150;
                dockingWindow.SetFloatMode(floatRectangle);
            } 
            else
            {
                //if already exists just set it visible
                dockingWindow.Visible = true;
            }

            dockingWindow.Label = windowTitle;
            dockingWindow.Content = editor as Widget;

            return true;
        }

        #region Render Columns Methods

        private void RenderName(TreeViewColumn column, CellRenderer cell, TreeModel model, TreeIter iter)
        {
            WorkspaceUnit workspaceUnit = (WorkspaceUnit)model.GetValue(iter, 0);
            (cell as CellRendererText).Text = workspaceUnit.FriendlyUnitName;
        }

        private void RenderType(TreeViewColumn column, CellRenderer cell, TreeModel model, TreeIter iter)
        {
            WorkspaceUnit workspaceUnit = (WorkspaceUnit)model.GetValue(iter, 1);
            string type = TraceLab.Core.Utilities.TypesHelper.GetFriendlyName(workspaceUnit.Type.ToString());
            (cell as CellRendererText).Text = type;
        }

        private void RenderValue(TreeViewColumn column, CellRenderer cell, TreeModel model, TreeIter iter)
        {
            WorkspaceUnit workspaceUnit = (WorkspaceUnit)model.GetValue(iter, 2);
            CellRendererText renderer = (CellRendererText)cell;
            // HERZUM SPRINT 5.1: TLAB-170
            if (workspaceUnit!=null &&  workspaceUnit.Type!=null && renderer!=null)
                if(workspaceUnit.Type.IsPrimitive || workspaceUnit.Type == String.Empty.GetType()) 
                {
                    renderer.Visible = true;
                    // HERZUM SPRINT 2.6: TLAB-170
                    if (workspaceUnit.Data != null)
                        renderer.Text = workspaceUnit.Data.ToString ();
                    else
                        renderer.Text = "";
                    // END HERZUM SPRINT 2.6: TLAB-170
                } 
                else
                {
                    renderer.Visible = false;
                }
            // END HERZUM SPRINT 5.1: TLAB-170
        }
                
        private void RenderViewerIcon(TreeViewColumn column, CellRenderer cell, TreeModel model, TreeIter iter)
        {
            WorkspaceUnit workspaceUnit = (WorkspaceUnit)model.GetValue(iter, 2);

            if(!workspaceUnit.Type.IsPrimitive && workspaceUnit.Type != String.Empty.GetType()) 
            {
                bool success = false;
                CellRendererPixbuf renderer = (CellRendererPixbuf)cell;
                renderer.Visible = true;

                try 
                {
                    success = WorkspaceViewerLoader.CheckIfEditorExists(workspaceUnit.Type);
                }
                catch(Exception) {}

                if(success)
                {
                    renderer.Pixbuf = s_workspaceViewerIcon;
                }
                else
                {
                    renderer.Pixbuf = s_noViewerIcon;
                }
            }
            else
            {
                cell.Visible = false;
            }
        }

        #endregion

        #region Workspace Collection Changed Handling

        /// <summary>
        /// Handler executed when underlying workspace collection changed.
        /// It updates the store view model of the table.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void WorkspaceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddUnits(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RemoveUnits(e.OldItems);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    RemoveUnits(e.OldItems);
                    AddUnits(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Move:
                    RemoveUnits(e.OldItems);
                    AddUnits(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    ClearUnits();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Adds the units to the store view
        /// </summary>
        /// <param name="items">Items.</param>
        private void AddUnits(System.Collections.IList items) 
        {            
            //assure that value is set using GTK+ main loop thread to avoid any threading problems
            Gtk.Application.Invoke(delegate 
            {
                foreach(object item in items) 
                {
                    WorkspaceUnit unit = (WorkspaceUnit)item;
                    if (unit.RealUnitName.StartsWith(m_experimentId))
                    { 
                        //each column has it's own rendering of same unit
                        m_workspaceStore.AppendValues(unit, unit, unit);
                    }
                }
            });
        }

        /// <summary>
        /// Removes the corresponding TreeIters from the view model store of the table
        /// </summary>
        /// <param name="unitsToRemove">Units to remove.</param>
        private void RemoveUnits(System.Collections.IList unitsToRemove) 
        {
            //assure that value is set using GTK+ main loop thread to avoid any threading problems
            Gtk.Application.Invoke(delegate 
            {
                TreeIter iter;
                if(m_workspaceStore.GetIterFirst(out iter)) 
                {
                    int removedCount = 0;
                    do
                    {
                        WorkspaceUnit aUnit = (WorkspaceUnit)m_workspaceStore.GetValue(iter, 0);
                        
                        bool foundMatching = false;
                        
                        //check if any unit to remove matches this unit
                        foreach(object unit in unitsToRemove) 
                        {
                            if((WorkspaceUnit)unit == aUnit)
                                foundMatching = true;
                        }
                        
                        if(foundMatching) 
                        {
                            m_workspaceStore.Remove(ref iter);
                            removedCount++;
                        }

                    } while(m_workspaceStore.IterNext(ref iter) && removedCount < unitsToRemove.Count);
                }
            });
        }

        /// <summary>
        /// Clears entire store
        /// </summary>
        internal void ClearUnits()
        {
            //assure that value is set using GTK+ main loop thread to avoid any threading problems
            Gtk.Application.Invoke(delegate 
            {
                m_workspaceStore.Clear();
            });
        }

        #endregion

        // HERZUM SPRINT 3: TLAB-86
        internal bool SearchValueMetricVariable(string nameMetricVariable, out string typeMetricVariable, out string valueMetricVariable)
        {
            foreach (object[] unit in m_workspaceStore)
            {
                TraceLab.Core.Workspaces.WorkspaceUnit row = unit[0] as TraceLab.Core.Workspaces.WorkspaceUnit;
                if (row.FriendlyUnitName.Equals(nameMetricVariable))
                {
                    typeMetricVariable = row.Type.ToString();
                    valueMetricVariable = row.Data.ToString();
                    return true;
                }
            }

            typeMetricVariable = "";
            valueMetricVariable = "";
            return false;

        }
        // END HERZUM SPRINT 3: TLAB-86

        private bool m_initialized = false;
        private DockFrame m_dockFrame;
        private string m_experimentId; //stores the id to the experiment which current workspace window is associated with
        private WorkspaceViewModel m_workspaceViewModel;
        private Gtk.ListStore m_workspaceStore;
        private Gtk.TreeView m_treeView;
        private static Gdk.Pixbuf s_workspaceViewerIcon;
        private static Gdk.Pixbuf s_noViewerIcon;
    }

    class CellRendererRouter : CellRenderer
    {
    }
}

