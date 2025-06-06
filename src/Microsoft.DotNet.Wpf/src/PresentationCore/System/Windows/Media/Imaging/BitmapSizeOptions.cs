﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MS.Internal;

namespace System.Windows.Media.Imaging
{
    #region BitmapSizeOptions

    /// <summary>
    /// Sizing options for an bitmap.  The resulting bitmap
    /// will be scaled based on these options.
    /// </summary>
    public class BitmapSizeOptions
    {
        /// <summary>
        /// Construct an BitmapSizeOptions object.  Still need to set the Width and Height Properties.
        /// </summary>
        private BitmapSizeOptions()
        {
        }

        /// <summary>
        /// Whether or not to preserve the aspect ratio of the original
        /// bitmap.  If so, then the PixelWidth and PixelHeight are
        /// maximum values for the bitmap size.  The resulting bitmap
        /// is only guaranteed to have either its width or its height
        /// match the specified values.  For example, if you want to
        /// specify the height, while preserving the aspect ratio for
        /// the width, then set the height to the desired value, and
        /// set the width to Int32.MaxValue.
        ///
        /// If we are not to preserve aspect ratio, then both the
        /// specified width and the specified height are used, and
        /// the bitmap will be stretched to fit both those values.
        /// </summary>
        public bool PreservesAspectRatio
        {
            get
            {
                return _preservesAspectRatio;
            }
        }

        /// <summary>
        /// PixelWidth of the resulting bitmap.  See description of
        /// PreserveAspectRatio for how this value is used.
        ///
        /// PixelWidth must be set to a value greater than zero to be valid.
        /// </summary>
        public int PixelWidth
        {
            get
            {
                return _pixelWidth;
            }
        }

        /// <summary>
        /// PixelHeight of the resulting bitmap.  See description of
        /// PreserveAspectRatio for how this value is used.
        ///
        /// PixelHeight must be set to a value greater than zero to be valid.
        /// </summary>
        public int PixelHeight
        {
            get
            {
                return _pixelHeight;
            }
        }

        /// <summary>
        /// Rotation to rotate the bitmap.  Only multiples of 90 are supported.
        /// </summary>
        public Rotation Rotation
        {
            get
            {
                return _rotationAngle;
            }
        }

        /// <summary>
        /// Constructs an identity BitmapSizeOptions (when passed to a TransformedBitmap, the
        /// input is the same as the output).
        /// </summary>
        public static BitmapSizeOptions FromEmptyOptions()
        {
            BitmapSizeOptions sizeOptions = new BitmapSizeOptions
            {
                _rotationAngle = Rotation.Rotate0,
                _preservesAspectRatio = true,
                _pixelHeight = 0,
                _pixelWidth = 0
            };

            return sizeOptions;
        }

        /// <summary>
        /// Constructs an BitmapSizeOptions that preserves the aspect ratio and enforces a height of pixelHeight.
        /// </summary>
        /// <param name="pixelHeight">Height of the resulting Bitmap</param>
        public static BitmapSizeOptions FromHeight(int pixelHeight)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pixelHeight);

            BitmapSizeOptions sizeOptions = new BitmapSizeOptions
            {
                _rotationAngle = Rotation.Rotate0,
                _preservesAspectRatio = true,
                _pixelHeight = pixelHeight,
                _pixelWidth = 0
            };

            return sizeOptions;
        }

        /// <summary>
        /// Constructs an BitmapSizeOptions that preserves the aspect ratio and enforces a width of pixelWidth.
        /// </summary>
        /// <param name="pixelWidth">Width of the resulting Bitmap</param>
        public static BitmapSizeOptions FromWidth(int pixelWidth)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pixelWidth);

            BitmapSizeOptions sizeOptions = new BitmapSizeOptions
            {
                _rotationAngle = Rotation.Rotate0,
                _preservesAspectRatio = true,
                _pixelWidth = pixelWidth,
                _pixelHeight = 0
            };

            return sizeOptions;
        }

        /// <summary>
        /// Constructs an BitmapSizeOptions that does not preserve the aspect ratio and
        /// instead uses dimensions pixelWidth x pixelHeight.
        /// </summary>
        /// <param name="pixelWidth">Width of the resulting Bitmap</param>
        /// <param name="pixelHeight">Height of the resulting Bitmap</param>
        public static BitmapSizeOptions FromWidthAndHeight(int pixelWidth, int pixelHeight)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pixelWidth);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pixelHeight);

            BitmapSizeOptions sizeOptions = new BitmapSizeOptions
            {
                _rotationAngle = Rotation.Rotate0,
                _preservesAspectRatio = false,
                _pixelWidth = pixelWidth,
                _pixelHeight = pixelHeight
            };

            return sizeOptions;
        }

        /// <summary>
        /// Constructs an BitmapSizeOptions that does not preserve the aspect ratio and
        /// instead uses dimensions pixelWidth x pixelHeight.
        /// </summary>
        /// <param name="rotation">Angle to rotate</param>
        public static BitmapSizeOptions FromRotation(Rotation rotation)
        {
            switch(rotation)
            {
                case Rotation.Rotate0:
                case Rotation.Rotate90:
                case Rotation.Rotate180:
                case Rotation.Rotate270:
                    break;
                default:
                    throw new ArgumentException(SR.Image_SizeOptionsAngle, nameof(rotation));
            }

            BitmapSizeOptions sizeOptions = new BitmapSizeOptions
            {
                _rotationAngle = rotation,
                _preservesAspectRatio = true,
                _pixelWidth = 0,
                _pixelHeight = 0
            };

            return sizeOptions;
        }

        // Note: In this method, newWidth, newHeight are not affected by the
        // rotation angle.
        internal void GetScaledWidthAndHeight(
            uint width,
            uint height,
            out uint newWidth,
            out uint newHeight)
        {
            if (_pixelWidth == 0 && _pixelHeight != 0)
            {
                Debug.Assert(_preservesAspectRatio);

                newWidth = (uint)((_pixelHeight * width)/height);
                newHeight = (uint)_pixelHeight;
            }
            else if (_pixelWidth != 0 && _pixelHeight == 0)
            {
                Debug.Assert(_preservesAspectRatio);

                newWidth = (uint)_pixelWidth;
                newHeight = (uint)((_pixelWidth * height)/width);
            }
            else if (_pixelWidth != 0 && _pixelHeight != 0)
            {
                Debug.Assert(!_preservesAspectRatio);

                newWidth = (uint)_pixelWidth;
                newHeight = (uint)_pixelHeight;
            }
            else
            {
                newWidth = width;
                newHeight = height;
            }
        }

        internal bool DoesScale
        {
            get
            {
                return (_pixelWidth != 0 || _pixelHeight != 0);
            }
        }

        internal WICBitmapTransformOptions WICTransformOptions
        {
            get
            {
                WICBitmapTransformOptions options = 0;

                switch (_rotationAngle)
                {
                    case Rotation.Rotate0:
                        options = WICBitmapTransformOptions.WICBitmapTransformRotate0;
                        break;
                    case Rotation.Rotate90:
                        options = WICBitmapTransformOptions.WICBitmapTransformRotate90;
                        break;
                    case Rotation.Rotate180:
                        options = WICBitmapTransformOptions.WICBitmapTransformRotate180;
                        break;
                    case Rotation.Rotate270:
                        options = WICBitmapTransformOptions.WICBitmapTransformRotate270;
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }

                return options;
            }
        }

        private bool        _preservesAspectRatio;
        private int         _pixelWidth;
        private int         _pixelHeight;
        private Rotation    _rotationAngle;
    }

    #endregion // BitmapSizeOptions
}

