//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a �t� g�n�r� � partir d'un mod�le.
//
//     Des modifications manuelles apport�es � ce fichier peuvent conduire � un comportement inattendu de votre application.
//     Les modifications manuelles apport�es � ce fichier sont remplac�es si le code est r�g�n�r�.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MovieNetData
{
    using System;
    using System.Collections.Generic;

    public partial class Film
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Film()
        {
            this.Comment = new HashSet<Comment>();
        }

        public Film(string title, string genres, string synopsis, string director)
        {
            this.Title = title;
            this.Genres = genres;
            this.Synopsis = synopsis;
            this.Director = director;
            this.Comment = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Genres { get; set; }
        public string Synopsis { get; set; }
        public Nullable<double> Score { get; set; }
        public Nullable<short> Year { get; set; }
        public string Director { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comment { get; set; }
    }
}
