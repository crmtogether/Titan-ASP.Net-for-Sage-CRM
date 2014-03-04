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
    class SageCRMDataSourceDesigner : DataSourceDesigner
    {
        private DesignerVerbCollection _designerverbs;
        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (_designerverbs == null)
                {
                    _designerverbs = new DesignerVerbCollection();
                    _designerverbs.Add(new DesignerVerb("Sage CRM List Properties...",
                        new EventHandler(this.OnPropertyBuilder)));
                }
                return _designerverbs;
            }
        }
        private void OnPropertyBuilder(object sender, EventArgs e)
        {
            SageCRMComponentDataSourcePropEditor compEditor1 = new SageCRMComponentDataSourcePropEditor();
            compEditor1.EditComponent(Component);
        }
        public override void Initialize(IComponent component)
        {
            if (!(component is SageCRMDataSource))
            {
                throw new ArgumentException("Component must be a Sage CRM Data Source", "component");
            }
            base.Initialize(component);
        }
    }
}
