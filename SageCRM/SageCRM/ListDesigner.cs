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
    class ListDesigner : ControlDesigner
    {
        private DesignerVerbCollection _designerverbs;
        public override DesignerVerbCollection Verbs 
        {
            get {
                if (_designerverbs==null)
                {
                    _designerverbs = new DesignerVerbCollection();
                    _designerverbs.Add(new DesignerVerb("Sage CRM List Properties...",
                        new EventHandler(this.OnPropertyBuilder)));
                    _designerverbs.Add(new DesignerVerb("Sage CRM List Builder...",
                        new EventHandler(this.OnSageCRMPropertyBuilder)));
                }
                return _designerverbs;
            }
        }
        private void OnSageCRMPropertyBuilder(object sender, EventArgs e)
        {
            SageCRMComponentListEditor compEditor = new SageCRMComponentListEditor();
            compEditor.EditComponent(Component);
        }
        private void OnPropertyBuilder(object sender, EventArgs e)
        {
            SageCRMComponentListPropEditor compEditor1 = new SageCRMComponentListPropEditor();
            compEditor1.EditComponent(Component);
        }
        public override void Initialize(IComponent component)
        {
            
            if ((!(component is SageCRMBaseListBlock)) &&
                (!(component is SageCRMListBlock)) && 
                (!(component is SageCRMPortalListBlock)))
            {
                throw new ArgumentException("Component must be a SageCRMlistblock","component");
            }
              
            base.Initialize(component);
        }
    }
}
