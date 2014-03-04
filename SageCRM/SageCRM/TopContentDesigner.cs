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
    class TopContentDesigner : ControlDesigner
    {
        private DesignerVerbCollection _designerverbs;
        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (_designerverbs == null)
                {
                    _designerverbs = new DesignerVerbCollection();
                    _designerverbs.Add(new DesignerVerb("Sage CRM Top Content Properties...",
                        new EventHandler(this.OnPropertyBuilder)));
                    _designerverbs.Add(new DesignerVerb("Sage CRM Top Content Builder...",
                        new EventHandler(this.OnSageCRMPropertyBuilder)));
                }
                return _designerverbs;
            }
        }
        private void OnSageCRMPropertyBuilder(object sender, EventArgs e)
        {
            SageCRMComponentTopContentEditor compEditor = new SageCRMComponentTopContentEditor();
            compEditor.EditComponent(Component);
        }
        private void OnPropertyBuilder(object sender, EventArgs e)
        {
            SageCRMComponentTopContentPropEditor compEditor1 = new SageCRMComponentTopContentPropEditor();
            compEditor1.EditComponent(Component);
        }
        public override void Initialize(IComponent component)
        {
            if (!(component is SageCRMTopContent))
            {
                throw new ArgumentException("Component must be a SageCRMTopContent", "component");
            }
            base.Initialize(component);
        }
    }
}
