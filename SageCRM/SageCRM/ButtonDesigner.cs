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
    class ButtonDesigner : ControlDesigner
    {
        private DesignerVerbCollection _designerverbs;
        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (_designerverbs == null)
                {
                    _designerverbs = new DesignerVerbCollection();
                    _designerverbs.Add(new DesignerVerb("Sage CRM Button Properties...",
                        new EventHandler(this.OnPropertyBuilder)));
                }
                return _designerverbs;
            }
        }
        private void OnPropertyBuilder(object sender, EventArgs e)
        {
            SageCRMComponentButtonPropEditor compEditor1 = new SageCRMComponentButtonPropEditor();
            compEditor1.EditComponent(Component);
        }
        public override void Initialize(IComponent component)
        {
            if (!(component is SageCRMButton))
            {
                throw new ArgumentException("Component must be a Sage CRM Button", "component");
            }
            base.Initialize(component);
        }
    }
}
