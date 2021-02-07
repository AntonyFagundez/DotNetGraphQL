using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotChocolate;

namespace GraphQLApp.DataAccess.Models
{
    // [GraphQLDescription("Represents any software or service that has a comand line interface.")]
    //Se pueden colocar las descripciones para GraphQl como decoradores de las propiedades
    public class Platform 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string LicenseKey { get; set; }


        public ICollection<Command> Commands {get; set;} = new List<Command>();

    }
}