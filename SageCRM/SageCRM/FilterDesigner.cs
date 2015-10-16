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
    class FilterDesigner : ControlDesigner
    {
        private DesignerVerbCollection _designerverbs;
        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (_designerverbs == null)
                {
                    _designerverbs = new DesignerVerbCollection();
                    _designerverbs.Add(new DesignerVerb("Sage CRM Filter Properties...",
                        new EventHandler(this.OnSageCRMPropertyBuilder)));
                    _designerverbs.Add(new DesignerVerb("Sage CRM Filter Builder...",
                        new EventHandler(this.OnPropertyBuilder)));
                }
                return _designerverbs;
            }
        }
        private void OnSageCRMPropertyBuilder(object sender, EventArgs e)
        {
            SageCRMComponentFilterPropEditor compEditor = new SageCRMComponentFilterPropEditor();
            compEditor.EditComponent(Component);
        }
        private void OnPropertyBuilder(object sender, EventArgs e)
        {
            SageCRMComponentFilterBlockEditor compEditor = new SageCRMComponentFilterBlockEditor();
            compEditor.EditComponent(Component);
        }
        public override void Initialize(IComponent component)
        {
            if ( (!(component is SageCRMBaseFilterBlock)) && 
                 (!(component is SageCRMFilterBlock)) &&
                  (!(component is SageCRMPortalFilterBlock)))
            {
                throw new ArgumentException("Component must be a SageCRMFilterBlock or SageCRMPortalFilterBlock", "component");
            }
            base.Initialize(component);
        }
    }
}
