using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Graph
{
    /// <summary>
    /// Class representing a collection of node labels
    /// </summary>
    /// <typeparam name="TNodeId">The node Id type</typeparam>
    public class NodeLabels<TNodeId>
    {
        #region Fields

        private readonly SortedList<TNodeId, string> _labels; //stores the collection of lables

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the collection of labels
        /// </summary>
        public IEnumerable<KeyValuePair<TNodeId, string>> Labels { get { return this._labels; } }

        /// <summary>
        /// Gets the count of labels in the collection
        /// </summary>
        public int Count { get { return this._labels.Count; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new <see cref="NodeLabels{TNodeId}"/>
        /// </summary>
        public NodeLabels()
        {
            this._labels = [];
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Adds a label to the collection
        /// </summary>
        /// <param name="id">The node id</param>
        /// <param name="label">The node label</param>
        public void AddLabel(TNodeId id, string label)
        {
            int index = this._labels.IndexOfKey(id);

            if (index == -1)
            {
                this._labels.Add(id, label);
            }
            else
            {
                this._labels[id] = label;
            }         
        }

        /// <summary>
        /// Gets the label of the node with the supplied id
        /// </summary>
        /// <param name="id">The node id</param>
        /// <returns><see cref="string"/> containing the node label</returns>
        public string GetLabel(TNodeId id)
        {
            return this._labels[id];
        }

        #endregion Methods
    }
}
