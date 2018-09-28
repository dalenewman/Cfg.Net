// Cfg.Net
// An Alternative .NET Configuration Handler
// Copyright 2015-2018 Dale Newman
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   
//       http://www.apache.org/licenses/LICENSE-2.0
//   
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System;
using System.Linq;
using System.Text;
using Cfg.Net.Loggers;
using Cfg.Net.Parsers;

namespace Cfg.Net.Ext {

    public static class Extensions {

        /// <summary>
        /// When you want to clone yourself 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static T Clone<T>(this T node) where T : CfgNode {
            return CfgMetadataCache.Clone(node);
        }

        public static void TrimEnd(this StringBuilder sb, string trimChars) {
            var length = sb.Length;
            if (length != 0) {
                var chars = trimChars.ToCharArray();
                var i = length - 1;

                if (trimChars.Length == 1) {
                    while (i > -1 && sb[i] == trimChars[0]) {
                        i--;
                    }
                } else {
                    while (i > -1 && chars.Any(c => c.Equals(sb[i]))) {
                        i--;
                    }
                }

                if (i < (length - 1)) {
                    sb.Remove(i + 1, (length - i) - 1);
                }
            }
        }


    }
}