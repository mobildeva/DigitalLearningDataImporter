using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DigitalLearningIntegration.Infraestructure.Utils
{
    public static class Utils
    {
        public static string CleanString(string name)
        {
            var cleanRes = Regex.Replace(name.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "").Replace(" ", string.Empty).Trim();

            return cleanRes;
        }
    }
}
