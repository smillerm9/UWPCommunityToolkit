﻿using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Microsoft.Toolkit.Uwp.UI
{
    /// <summary>
    /// Provides methods and tools to cache files in a folder
    /// </summary>
    public class ImageCache : CacheBase<BitmapImage>
    {
        /// <summary>
        /// Private singleton field.
        /// </summary>
        private static ImageCache _instance;

        /// <summary>
        /// Gets public singleton property.
        /// </summary>
        public static ImageCache Instance => _instance ?? (_instance = new ImageCache());

        /// <summary>
        /// Cache specific hooks to proccess items from http response
        /// </summary>
        /// <param name="stream">inpupt stream</param>
        /// <returns>awaitable task</returns>
        protected override async Task<BitmapImage> InitializeTypeAsync(IRandomAccessStream stream)
        {
            // nothing to do in this instance;
            BitmapImage image = new BitmapImage();
            await image.SetSourceAsync(stream);

            return image;
        }

        /// <summary>
        /// Cache specific hooks to proccess items from http response
        /// </summary>
        /// <param name="baseFile">storage file</param>
        /// <returns>awaitable task</returns>
        protected override async Task<BitmapImage> InitializeTypeAsync(StorageFile baseFile)
        {
            using (var stream = await baseFile.OpenReadAsync().AsTask().ConfigureAwait(false))
            {
                return await InitializeTypeAsync(stream);
            }
        }
    }
}
