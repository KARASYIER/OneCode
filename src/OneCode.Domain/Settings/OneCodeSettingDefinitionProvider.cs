using Volo.Abp.Settings;

namespace OneCode.Settings
{
    public class OneCodeSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(OneCodeSettings.MySetting1));
        }
    }
}
