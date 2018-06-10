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

    public partial class Comment
    {
        public Comment(Nullable<double> score, string text, User user, Film film)
        {
            this.Score = score;
            this.Text = text;
            this.User = user;
            this.Film = film;
            this.CreationDate = DateTime.Now;
            this.ModificationDate = DateTime.Now;
        }

        public Comment(string text, User user, Film film) : this(null, text, user, film) { }

        public Comment() { }

        public int Id { get; set; }
        public Nullable<double> Score { get; set; }
        public string Text { get; set; }
        public System.DateTime CreationDate { get; set; }
        public System.DateTime ModificationDate { get; set; }

        public virtual User User { get; set; }
        public virtual Film Film { get; set; }
    }
}