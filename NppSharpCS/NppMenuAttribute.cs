using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NppSharp
{
    /// <summary>
    /// Specifies that a command is to be placed into its own menu.
    /// </summary>
    public class NppMenuAttribute : Attribute
    {
        private string _name;
        private string _insertBefore = "";

        /// <summary>
        /// Specifies the menu under which this command will appear.
        /// </summary>
        /// <param name="name">The name of the menu.</param>
        /// <remarks>If a menu with that name does not yet exist, it will be created.</remarks>
        public NppMenuAttribute(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Gets or sets the name of the menu.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets or sets the name of the menu where a new menu (if required) will be inserted before.
        /// </summary>
        public string InsertBefore
        {
            get { return _insertBefore; }
            set { _insertBefore = value; }
        }
    }
}
