using System;
using System.Collections.Generic;
using UnityEngine;

namespace Codebase.Core.Services.ArrayCrawler
{
    public class ArrayCrawler<T>
    {
        private readonly Func<T, bool> _isEmpty;

        public ArrayCrawler() : this(obj => EqualityComparer<T>.Default.Equals(obj, default))
        {
        }

        public ArrayCrawler(Func<T, bool> isEmpty) =>
            _isEmpty = isEmpty;

        public T[][,] Crawl(T[,] array)
        {
            HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
            Dictionary<int, List<Entry>> islandsData = new Dictionary<int, List<Entry>>();

            FillIslands(array, visited, islandsData);

            List<T[,]> islands = HandleIslandsData(islandsData);

            return islands.ToArray();
        }

        private void FillIslands(T[,] array, HashSet<Vector2Int> visited, Dictionary<int, List<Entry>> islands)
        {
            Vector2Int size = new Vector2Int(array.GetLength(0), array.GetLength(1));

            int islandIndex = 0;

            for (int x = 0; x < size.x; x++)
            for (int y = 0; y < size.y; y++)
            {
                if (visited.Contains(new Vector2Int(x, y)))
                    continue;
                
                T element = array[x, y];
                
                if(_isEmpty(element))
                    continue;

                islandIndex++;

                List<Entry> entries = new List<Entry>();

                CrawlNeighborsRecursively(array, visited, entries, new Vector2Int(x, y));
                islands.Add(islandIndex, entries);
            }
        }

        private void CrawlNeighborsRecursively
        (
            T[,] array,
            HashSet<Vector2Int> visited,
            List<Entry> entries,
            Vector2Int position
        )
        {
            for (int dx = -1; dx <= 1; dx++)
            for (int dy = -1; dy <= 1; dy++)
            {
                Vector2Int currentPosition = position + new Vector2Int(dx, dy);

                if ((dx == 1 && dy == 1) || (dx == -1 && dy == -1) || (dx == -1 && dy == 1) || (dx == 1 && dy == -1))
                    continue;

                if (IsInBounds(array, currentPosition) == false || visited.Contains(currentPosition))
                {
                    visited.Add(currentPosition);

                    continue;
                }

                T element = array[currentPosition.x, currentPosition.y];

                if (_isEmpty.Invoke(element))
                {
                    visited.Add(currentPosition);

                    continue;
                }

                visited.Add(currentPosition);
                entries.Add(new Entry(currentPosition, element));
                CrawlNeighborsRecursively(array, visited, entries, currentPosition);
            }
        }

        private List<T[,]> HandleIslandsData(Dictionary<int, List<Entry>> islandsData)
        {
            List<T[,]> islands = new List<T[,]>();

            foreach ((int _, List<Entry> entries) in islandsData)
            {
                Vector2Int min = new Vector2Int(int.MaxValue, int.MaxValue);
                Vector2Int max = new Vector2Int(int.MinValue, int.MinValue);

                foreach (Entry entry in entries)
                {
                    min = Vector2Int.Min(min, entry.Position);
                    max = Vector2Int.Max(max, entry.Position);
                }

                Vector2Int size = max - min + new Vector2Int(1, 1);

                T[,] island = new T[size.x, size.y];

                foreach (Entry entry in entries)
                {
                    island[entry.Position.x - min.x, entry.Position.y - min.y] = entry.Value;
                }

                islands.Add(island);
            }

            return islands;
        }

        private bool IsInBounds(T[,] array, Vector2Int position) =>
            position.x >= 0 && position.x < array.GetLength(0) && position.y >= 0 && position.y < array.GetLength(1);

        private class Entry
        {
            public Entry(Vector2Int position, T value)
            {
                Position = position;
                Value = value;
            }

            public Vector2Int Position;
            public T Value;
        }
    }
}
