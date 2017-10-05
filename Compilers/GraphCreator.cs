using Shields.GraphViz.Components;
using Shields.GraphViz.Models;
using Shields.GraphViz.Services;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Compilers
{
    /**
     * Utility class for create graph from a list of symbols
     * (Usually posfix)
     * */
    class GraphCreator
    {

        List<Symbol> symbols;
        List<Image> images;
        IRenderer renderer;

        internal List<Symbol> Symbols { get => symbols; set => symbols = value; }
        internal List<Image> Images { get => images; set => images = value; }

        public GraphCreator()
        {
            Symbols = new List<Symbol>(); ;
            Images = new List<Image>();
            renderer = new Renderer(@"C:\Program Files (x86)\Graphviz2.38\bin");
        }

        /**
         * Iterate over symbols and create image depending on symbol type(Operator, non-Operator, etc.)
         * */
        public async Task CreateImages()
        {
            //images.Clear();
            int i = 0;
            bool busy = false;

            /*while (i < symbols.Count)
            {
                while (busy)
                {
                    Console.WriteLine("Esperando....");
                }
                Symbol s = Symbols[i];

                busy = true;
                if (!s.IsOperator())
                {
                    Graph graph = CreateRegularGraph(s, s.Coef);
                    busy = await CreateImageFromGraph(graph, i);
                }
                else if (s.Coef.Equals("+"))
                {
                    // Add positive kleene closure to previous symbol
                    Graph graph = CreateRegularGraph(Symbols[i - 1], s.Coef);
                    var ep = ImmutableDictionary.CreateBuilder<Id, Id>();

                    ep.Add("label", "ε");
                    graph.Add(new EdgeStatement("3", "2", ep.ToImmutable()));
                    busy = await CreateImageFromGraph(graph, i);
                }
                else if (s.Coef.Equals("*"))
                {
                    // Add star kleene closure to previous symbol
                    Graph graph = CreateRegularGraph(Symbols[i - 1], s.Coef);
                    var ep = ImmutableDictionary.CreateBuilder<Id, Id>();

                    ep.Add("label", "ε");
                    graph.Add(new EdgeStatement("3", "2", ep.ToImmutable()));
                    graph.Add(new EdgeStatement("1", "4", ep.ToImmutable()));
                    busy = await CreateImageFromGraph(graph, i);
                } else
                {
                    busy = false;
                }
                i++;
            }*/

            var ep = ImmutableDictionary.CreateBuilder<Id, Id>();
            ep.Add("label", "ε");

            foreach (Symbol s in Symbols)
            {
                if (!s.IsOperator())
                {
                    Graph graph = CreateRegularGraph(s, s.Coef);
                    await CreateImageFromGraph(graph, i);
                } else if(s.Coef.Equals("+"))
                {
                    // Add positive kleene closure to previous symbol
                    Graph graph = CreateRegularGraph(Symbols[i - 1], s.Coef)
                                  .Add(new EdgeStatement("3", "2", ep.ToImmutable()));
                    await CreateImageFromGraph(graph, i);
                } else if (s.Coef.Equals("*"))
                {
                    // Add star kleene closure to previous symbol
                    Graph graph = CreateRegularGraph(Symbols[i - 1], s.Coef)
                                  .Add(new EdgeStatement("3", "2", ep.ToImmutable()))
                                  .Add(new EdgeStatement("1", "4", ep.ToImmutable()));
                    await CreateImageFromGraph(graph, i);
                }
                i++;
            }
            Console.WriteLine("FINISH");
        }

        /**
         * Create graph with 4 nodes, 2 epsilon edges and one coef
         *         
         *         e      S      e
         *      a ---> b ---> c ---> d
         **/
        private Graph CreateRegularGraph(Symbol s, String title)
        {
            var ep = ImmutableDictionary.CreateBuilder<Id, Id>();
            ep.Add("label", "ε");

            var label = ImmutableDictionary.CreateBuilder<Id, Id>();
            label.Add("label", s.Coef);

            return Graph.Directed
                .Add(AttributeStatement.Graph.Set("rankdir", "LR"))
                .Add(AttributeStatement.Graph.Set("labelloc", "t"))
                //.Add(AttributeStatement.Graph.Set("bgcolor", "#34495e"))
                .Add(AttributeStatement.Node.Set("style", "filled"))
                .Add(AttributeStatement.Node.Set("fillcolor", "#ECECEC"))
                .Add(AttributeStatement.Graph.Set("label", "Graph for " + title))
                .Add(new EdgeStatement("1", "2", ep.ToImmutable()))
                .Add(new EdgeStatement("2", "3", label.ToImmutable()))
                .Add(new EdgeStatement("3", "4", ep.ToImmutable()));
        }

        /**
         * Create image asynchronously from graph and add it to list when ready
         * */
        private async Task CreateImageFromGraph(Graph graph, int i)
        {
            using (Stream file = new MemoryStream())
            {
                await renderer.RunAsync(
                    graph, file,
                    RendererLayouts.Dot,
                    RendererFormats.Png,
                    CancellationToken.None);

                Images.Insert(i, Image.FromStream(file));
                Console.WriteLine("Se inserto grafo en " + i);
            }
        }
    }
}
