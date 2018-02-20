using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace ExampleProject.Handlers
{
    // I isolated this in its own class due to conflicts between ErrorEventArgs
    // in Newtonsoft and System.IO. I prefer to have a class full of static
    // error handlers than code cluttered with explicit namespaces outside of
    // the top using block.
    public class JsonErrorHandler
    {
        public static void HandleDeserializationError(object sender, ErrorEventArgs args)
        {
            // Log and handle the error, we want deserialization to proceed
            // so the rest of the JSON can parse into a C# object if possible
            // TODO: Logging
            Console.WriteLine(args.ErrorContext.Error.Message);
            args.ErrorContext.Handled = true;
        }
    }
}
