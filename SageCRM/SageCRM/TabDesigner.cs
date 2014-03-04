using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.Design.WebControls;
using System.Web.UI.WebControls;
using SageCRM.AspNet;

namespace SageCRM.AspNet.Design
{
    class TabDesigner : ControlDesigner
    {
        private DesignerVerbCollection _designerverbs;
        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (_designerverbs == null)
                {
                    _designerverbs = new DesignerVerbCollection();
                    _designerverbs.Add(new DesignerVerb("Sage CRM Tab Properties...",
                        new EventHandler(this.OnPropertyBuilder)));
                    _designerverbs.Add(new DesignerVerb("Sage CRM Tab Builder...",
                        new EventHandler(this.OnSageCRMPropertyBuilder)));
                }
                return _designerverbs;
            }
        }
        private void OnSageCRMPropertyBuilder(object sender, EventArgs e)
        {
            SageCRMComponentTabGroupEditor compEditor = new SageCRMComponentTabGroupEditor();
            compEditor.EditComponent(Component);
        }
        private void OnPropertyBuilder(object sender, EventArgs e)
        {
            SageCRMComponentTabPropEditor compEditor1 = new SageCRMComponentTabPropEditor();
            compEditor1.EditComponent(Component);
        }
        public override void Initialize(IComponent component)
        {
            if (!(component is SageCRMTabGroup))
            {
                throw new ArgumentException("Component must be a Sage CRM tab group", "component");
            }
            base.Initialize(component);
        }
    }
}
