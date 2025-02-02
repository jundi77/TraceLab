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
//
using System;
using TraceLab.Core.Components;
using System.Collections.Generic;
using TraceLab.Core.Experiments;
using Gtk;

namespace TraceLab.UI.GTK
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class LoopDecisionInfoPanel : Gtk.EventBox
    {
        
        private string LOAD_A_VALUE = "Load a value";

        public LoopDecisionInfoPanel()
        {
            this.Build();
            //set EventBox VisibleWindow to invisible so it is only used to trap events
            this.VisibleWindow = false;
            this.AboveChild = false;
            this.errorTextView1.InitColorTags();

            this.VisibilityNotifyEvent += (object o, VisibilityNotifyEventArgs args) => {
                fillLoadComboBox ();
              //  hideOrShowErrorArea();
            };

            this.WidthRequest = 420;
            this.HeightRequest = 315;

            this.decisionCodeEntryBox1.Buffer.Changed += (object sender, EventArgs e) => {
                resetErrorTextField();

            };
        }

        private void hideOrShowErrorArea(){
            this.errorTextView1.Visible = false;
          //  this.GtkScrolledWindow.Visible = false;
            if(this.GdkWindow != null)
                this.GdkWindow.Resize (420, 315);             
        }

        private void resetErrorTextField(){
            this.errorTextView1.Buffer.Text = "";
        }

        public LoopDecisionInfoPanel(ApplicationContext applicationContext) : this()
        {
            m_applicationContext = applicationContext;
        }

        private ApplicationContext m_applicationContext;
        private LoopScopeMetadata m_metadata;
        private LoopNodeControl m_loopControl;

        public LoopNodeControl LoopNodeControl 
        {
            get { return m_loopControl; }
            set 
            {
                if(m_loopControl != value) 
                {
                    m_loopControl = value;
                    m_metadata = m_loopControl.ExperimentNode.Data.Metadata as LoopScopeMetadata;

                    bool isEditable = m_loopControl.IsEditable;

                    SetDecisionData(isEditable);
                    SetComboBoxAction ();

                    this.LoopNodeControl.TextChanged += (object sender, EventArgs e) => {
                        this.componentLabelValue.Text = this.LoopNodeControl.Text;
                    };

                    RefreshError();

                    m_loopControl.ExperimentNode.ErrorChanged += HandleErrorChanged;
                }
            }
        }

        /// <summary>
        /// Displays error
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void HandleErrorChanged (object sender, ExperimentNodeErrorEventArgs e)
        {
            RefreshError();
        }

        private void RefreshError()
        {
            if (m_loopControl.ExperimentNode.HasError) {
                this.errorTextView1.ShowError(m_loopControl.ExperimentNode.ErrorMessage);
            }
            else {
                this.errorTextView1.Visible = false;
            }
        }

        private void SetDecisionData(bool isEditable)
        {
            this.componentLabelValue.Text = m_metadata.Label;
            this.componentLabelValue.IsEditable = isEditable;
            this.componentLabelValue.Sensitive = isEditable;

            this.decisionCodeEntryBox1.Buffer.Text = m_metadata.DecisionCode;
            this.decisionCodeEntryBox1.Editable = isEditable;
            this.decisionCodeEntryBox1.Sensitive = isEditable;

            this.decisionCodeEntryBox1.Buffer.Changed += (object sender, EventArgs e) => 
            {
                m_metadata.DecisionCode = this.decisionCodeEntryBox1.Buffer.Text;
            };
        }

        private void SetComboBoxAction ()
        {
            fillLoadComboBox ();

            m_applicationContext.Application.Experiment.VertexAdded += (ExperimentNode vertex) => {
                   fillLoadComboBox ();
            };

            m_applicationContext.Application.Experiment.VertexRemoved += (ExperimentNode vertex) => {
                fillLoadComboBox ();
            };
        }

        /// <summary>
        /// Handles the event of Check Code Button Click.
        /// It compiles decision code.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        protected void CheckCode (object sender, EventArgs e)
        {
            fillLoadComboBox ();

            List<string> workspaceTypeDirectories = m_applicationContext.Application.WorkspaceViewModel.WorkspaceTypeDirectories;
            Experiment experiment = m_applicationContext.Application.Experiment;
            LoggerNameRoot loggerNameRoot = new LoggerNameRoot(experiment.ExperimentInfo.Id);

            //compile decision - the result is set in metadata Compilation Status, and eventual message in Error
            TraceLab.Core.Decisions.DecisionCompilationRunner.CompileDecision(m_loopControl.ExperimentNode, 
                                                                              experiment, 
                                                                              workspaceTypeDirectories, 
                                                                              loggerNameRoot);

            //retrieve compilation status from metadata
            if(m_metadata.CompilationStatus == CompilationStatus.Successful) 
            {
                this.errorTextView1.ShowInfoMessage("Compilation successful!");
            } 
            //otherwise error displays on ErrorChanged event - see HandleErrorChanged method

            if(this.errorTextView1.Visible)
                this.GdkWindow.Resize (381, 375);
        }

        /// <summary>
        /// Handles the changed of the component label.
        /// It updates label in the model metadata
        /// </summary>
        /// <param name='sender'>
        /// Sender.
        /// </param>
        /// <param name='e'>
        /// E.
        /// </param>
        protected void labelChanged (object sender, EventArgs e)
        {
            String newLabel = this.componentLabelValue.Text;
            m_metadata.Label = newLabel;
            this.LoopNodeControl.Text = newLabel; 
        }     

        //utility method to clear a gtk combobox
        private void clearComboBox () {
            Gtk.ComboBox cb = this.loadComboBox;

            cb.Clear ();
            CellRendererText cell = new CellRendererText ();
            cb.PackStart (cell, false);
            cb.AddAttribute (cell, "text", 0);
            ListStore store = new ListStore (typeof(string));
            cb.Model = store;
        }

        private void fillLoadComboBox ()
        {
            clearComboBox ();

            ExperimentNode currentNode = LoopNodeControl.ExperimentNode;
            Dictionary<string, string> predeccessorsOutputsNameTypeLookup;

            // TLAB-174: we use the Owner instead of the application experiment to get only variables the scope can access
            //var availableInputMappingsPerNode = new TraceLab.Core.Utilities.InputMappings (m_applicationContext.Application.Experiment);
            var availableInputMappingsPerNode = new TraceLab.Core.Utilities.InputMappings (m_loopControl.ExperimentNode.Owner);

            if (availableInputMappingsPerNode.TryGetValue (currentNode, out predeccessorsOutputsNameTypeLookup) == false) {
                predeccessorsOutputsNameTypeLookup = new Dictionary<string, string> (); //return empty - there is not path from start node to decision
            }

            String wrapper = "Load(\"";
            this.loadComboBox.AppendText (LOAD_A_VALUE);
            foreach (string workspaceUnitName in predeccessorsOutputsNameTypeLookup.Keys) {
                this.loadComboBox.AppendText (wrapper + workspaceUnitName + "\")");
            }     

            setActiveComboBoxValue (loadComboBox, LOAD_A_VALUE);
        }

        //adds text to decision text area
        protected void OnAddToDecisionCodeButtonClicked (object sender, EventArgs e)
        {
            TreeIter tree;
                if (loadComboBox.GetActiveIter (out tree)) {
                    String textToAdd = (String)loadComboBox.Model.GetValue (tree, 0);
                    this.decisionCodeEntryBox1.Buffer.InsertAtCursor(textToAdd);
                }           
        }

        protected void OnLoadComboBoxChanged (object sender, EventArgs e)
        {
            TreeIter tree;
            if (loadComboBox.GetActiveIter (out tree)) {
                if ((String)loadComboBox.Model.GetValue (tree, 0) != LOAD_A_VALUE) {
                    this.decisionCodeEntryBox1.Buffer.InsertAtCursor ((String)loadComboBox.Model.GetValue (tree, 0));
                }
            }

            setActiveComboBoxValue (this.loadComboBox, LOAD_A_VALUE);
        }

        private void setActiveComboBoxValue (Gtk.ComboBox cb, string s)
        {            
            Gtk.TreeIter iter;
            cb.Model.GetIterFirst (out iter);
            do {
                GLib.Value thisRow = new GLib.Value ();
                cb.Model.GetValue (iter, 0, ref thisRow);
                if ((thisRow.Val as string).Equals (s)) {
                    cb.SetActiveIter (iter);
                    break;
                }
            } while (cb.Model.IterNext (ref iter));  
        }
    }
}

