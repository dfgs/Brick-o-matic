using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Brick_o_matic.Viewer.ViewModels
{
    [MarkupExtensionReturnType(typeof(object))]
    public abstract class ProjectMarkupExtension : MarkupExtension
    {
        [ConstructorArgument("path")]
        public PropertyPath Path { get; set; }

        protected ProjectMarkupExtension()
        {
        }

        protected ProjectMarkupExtension(PropertyPath path)
        {
            Path = path;
        }

 
       /* public IValueConverter Converter { get; set; }
        public object ConverterParameter { get; set; }
        public string ElementName { get; set; }
        public RelativeSource RelativeSource { get; set; }
        public object Source { get; set; }
        public bool ValidatesOnDataErrors { get; set; }
        public bool ValidatesOnExceptions { get; set; }
        [TypeConverter(typeof(CultureInfoIetfLanguageTagConverter))]
        public CultureInfo ConverterCulture { get; set; }*/

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var pvt = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            if (pvt == null) return null;

            var targetObject = pvt.TargetObject as DependencyObject;
            if (targetObject == null) return null;

            var targetProperty = pvt.TargetProperty as DependencyProperty;
            if (targetProperty == null) return null;

           

            return targetObject.GetValue(targetProperty);
        }

    }
}
