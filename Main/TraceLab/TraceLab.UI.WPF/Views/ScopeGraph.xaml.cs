﻿// TraceLab - Software Traceability Instrument to Facilitate and Empower Traceability Research
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

using TraceLab.Core.Experiments;
using TraceLab.UI.WPF.Controls.ZoomControl;

namespace TraceLab.UI.WPF.Views
{
    /// <summary>
    /// Interaction logic for ScopeGraph.xaml
    /// </summary>
    public partial class ScopeGraph : TraceLab.UI.WPF.Views.GraphView
    {
                /// <summary>
        /// Initializes a new instance of the <see cref="DockableGraph"/> class.
        /// </summary>
        public ScopeGraph() : base()
        {
            InitializeComponent();
            HACK_Vertex.SetValue(GraphSharp.Controls.VertexControl.VertexProperty, new ComponentNode("HACK_Node", new SerializedVertexData()));
            Focusable = true;
        }
        
        #region UI Elements

        protected override Controls.NodeGraphLayout GraphLayout
        {
            get { return graphLayout; }
        }

        protected override ZoomControl ZoomControl
        {
            get { return zoomControl; }
        }

        protected override GraphSharp.Controls.VertexControl HACK_VertexControl
        {
            get { return HACK_Vertex; }
        }

        protected override GraphSharp.Controls.EdgeControl HACK_EdgeControl
        {
            get { return HACK_Edge; }
        }

        protected override System.Windows.Shapes.Rectangle MarqueeAdorner
        {
            get { return marqueeAdorner; }
        }

        #endregion
    }
}
