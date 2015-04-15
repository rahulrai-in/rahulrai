﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Document.cs" company="Microsoft">
//   Copyright (c) Glasgow City Council. All Rights Reserved.
// </copyright>
// <summary>
//   The document.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RahulRai.Websites.Utilities.Common.Entities
{
    #region

    using System;

    #endregion

    /// <summary>
    ///     The document.
    /// </summary>
    public class Document
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the uri.
        /// </summary>
        public Uri Uri { get; set; }

        #endregion
    }
}