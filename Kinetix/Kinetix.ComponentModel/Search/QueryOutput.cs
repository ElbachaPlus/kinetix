﻿using System.Collections.Generic;

namespace Kinetix.ComponentModel.Search {

    /// <summary>
    /// Sortie d'une recherche avancée.
    /// </summary>
    /// <typeparam name="TDocument">Le type du document.</typeparam>
    public class QueryOutput<TDocument> {

        /// <summary>
        /// Liste de résultats (cas d'une recherche sans groupe).
        /// </summary>
        public ICollection<TDocument> List {
            get;
            set;
        }

        /// <summary>
        /// Groupe de liste de résultats (cas d'une recherche avec groupe).
        /// </summary>
        public GroupResultList<TDocument> Groups {
            get;
            set;
        }

        /// <summary>
        /// Facettes sélectionnées.
        /// </summary>
        public FacetListOutput Facets {
            get;
            set;
        }

        /// <summary>
        /// Nombre total d'éléments.
        /// </summary>
        public long? TotalCount {
            get;
            set;
        }

        /// <summary>
        /// Rappel de l'entrée de la recherche.
        /// </summary>
        public QueryInput Query {
            get;
            set;
        }
    }
}
