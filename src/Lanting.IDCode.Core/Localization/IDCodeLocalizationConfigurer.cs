using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Lanting.IDCode.Localization
{
    public static class IDCodeLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(IDCodeConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(IDCodeLocalizationConfigurer).GetAssembly(),
                        "Lanting.IDCode.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
