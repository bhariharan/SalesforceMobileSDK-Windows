﻿/*
 * Copyright (c) 2013, salesforce.com, inc.
 * All rights reserved.
 * Redistribution and use of this software in source and binary forms, with or
 * without modification, are permitted provided that the following conditions
 * are met:
 * - Redistributions of source code must retain the above copyright notice, this
 * list of conditions and the following disclaimer.
 * - Redistributions in binary form must reproduce the above copyright notice,
 * this list of conditions and the following disclaimer in the documentation
 * and/or other materials provided with the distribution.
 * - Neither the name of salesforce.com, inc. nor the names of its contributors
 * may be used to endorse or promote products derived from this software without
 * specific prior written permission of salesforce.com, inc.
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 */
﻿
using Salesforce.SDK.App;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Windows.Storage;
using Windows.UI.Xaml;

namespace Salesforce.SDK.Hybrid
{
    /// <summary>
    /// Helper for reading files from resources.
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        ///  Return string containing contents of resource file
        ///  Throws a FileNotFoundException if the file cannot be found
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadConfigFromResource(string path)
        {
            Assembly assembly = typeof(ConfigHelper).GetTypeInfo().Assembly;
            using (var resource = assembly.GetManifestResourceStream(path))
            {
                if (resource != null)
                {
                    using (var reader = new StreamReader(resource))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            throw new FileNotFoundException("Resource file not found", path);
        }

        public async static Task<string> ReadFileFromApplication(string path)
        {
            Uri fileUri = new Uri(@"ms-appx:///" + path);
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(fileUri);
            if (file != null)
            {
                var stream = await file.OpenStreamForReadAsync();
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            throw new FileNotFoundException("Resource file not found", path);
        }
    }
}