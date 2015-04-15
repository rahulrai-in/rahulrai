﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileLeaseState.cs" company="Microsoft">
//   Copyright (c) Glasgow City Council. All Rights Reserved.
// </copyright>
// <summary>
//   The file lease state.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RahulRai.Websites.Utilities.Common.Entities
{
    /// <summary>
    ///     The file lease state.
    /// </summary>
    public enum FileLeaseState
    {
        /// <summary>
        ///     The acquired.
        /// </summary>
        Acquired, 

        /// <summary>
        ///     The failed.
        /// </summary>
        Failed, 

        /// <summary>
        ///     The broken.
        /// </summary>
        Broken, 

        /// <summary>
        ///     The released.
        /// </summary>
        Released
    }
}