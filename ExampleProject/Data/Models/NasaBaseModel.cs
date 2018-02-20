using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleProject.Data.Models
{
    // Okay so there's really nothing going on here, but let's say we knew all
    // models had some traits in common. We might want to implement those as
    // properties here. For now, it's mostly here to ensure we considered that
    // whatever we're passing to the dao is actually some kind of model from
    // NASA's API. Unfortunately for me NASA's APIs don't return objects with a
    // lot of common properties and now I just look like I'm too stubborn to
    // break this pattern.
    public abstract class NasaBaseModel
    {
        // Take this as an example: if we had objects which consistently had
        // guid-based identifiers, we'd have it in every model without declaring
        // it in every inheriting class. We could also make sure that it was
        // always populated with the parameterless constructor that's called
        // whenever we create new instances of the inheriting classes.
//        public Guid Id { get; set; }
//
//        protected NasaBaseModel()
//        {
//            Id = Guid.NewGuid();
//        }
    }
}
