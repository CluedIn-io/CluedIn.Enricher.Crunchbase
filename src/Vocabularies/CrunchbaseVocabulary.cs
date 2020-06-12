// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClearBitVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the ClearBitVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Vocabularies
{
    /// <summary>The clear bit vocabulary.</summary>
    public static class CrunchbaseVocabulary
    {
        /// <summary>
        /// Initializes static members of the <see cref="CrunchbaseVocabulary" /> class.
        /// </summary>
        static CrunchbaseVocabulary()
        {
            Organization = new CrunchbaseOrganizationVocabulary();
            Person = new CrunchbasePersonVocabulary();
        }

        /// <summary>Gets the organization.</summary>
        /// <value>The organization.</value>
        public static CrunchbaseOrganizationVocabulary Organization { get; private set; }

        /// <summary>Gets the person.</summary>
        /// <value>The person.</value>
        public static CrunchbasePersonVocabulary Person { get; private set; }
    }
}