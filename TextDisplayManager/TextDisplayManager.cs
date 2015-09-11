using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Storage;

namespace TextDisplay
{
    public static class TextDisplayManager
    {
        private static IEnumerable<ITextDisplay> s_avaliableDisplays = null;
        public static IAsyncOperation<IEnumerable<ITextDisplay>> GetDisplays()
        {
            return Task.Run(async () =>
            {
                if (null == s_avaliableDisplays)
                {
                    var displays = new List<ITextDisplay>();

                    var folder = Windows.Storage.ApplicationData.Current.LocalSettings;
                    try
                    {
                        var playlistFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///TextDisplay/screens.config"));

                        var xmlString = await FileIO.ReadTextAsync(playlistFile);
                        var xml = XElement.Parse(xmlString);

                        var screensConfig = xml.Descendants("Screen");

                        foreach(var screenConfig in screensConfig)
                        {
                            var driverTypeAttribute = screensConfig.Attributes().Where(a => a.Name == "driverType").FirstOrDefault();

                            if (null != driverTypeAttribute)
                            {
                                var type = Type.GetType("TextDisplay." + driverTypeAttribute.Value);
                                ITextDisplay driver = Activator.CreateInstance(type, screenConfig) as ITextDisplay;
                                if (null != driver)
                                {
                                    displays.Add(driver);
                                }
                            }
                        }

                        s_avaliableDisplays = displays;

                    }
                    catch (FileNotFoundException)
                    {
                        Debug.WriteLine("TextDisplayManager: Screen config file not found");
                    }
                }

                return s_avaliableDisplays;
            }).AsAsyncOperation();
        }
    }
}
