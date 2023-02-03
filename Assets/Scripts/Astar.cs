﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    public static class Astar
    {
        static List<GMapUnit> openList = new List<GMapUnit>();
        static HashSet<GMapUnit> closeSet = new HashSet<GMapUnit>();

        static Dictionary<GMapUnit, (int g, int h)> costDic = new Dictionary<GMapUnit, (int g, int h)>();
        static Dictionary<GMapUnit, GMapUnit> parentDic = new Dictionary<GMapUnit, GMapUnit>();

        /// <summary>
        /// A*算法，寻找最短路径
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static List<Vector2Int> FindingPath(GMapUnit startNode, GMapUnit endNode)
        {
            int CalDis(Vector2Int a, Vector2Int b)
            {
                var dx = Mathf.Abs(a.x - b.x);
                var dy = Mathf.Abs(a.y - b.y);
                return Mathf.Max(dx, dy) * 10 + Mathf.Min(dx, dy) * 4;
            }

            openList.Clear();
            closeSet.Clear();
            costDic.Clear();
            parentDic.Clear();

            openList.Add(startNode);
            costDic[startNode] = (0, CalDis(endNode.pos, startNode.pos));

            while (openList.Count > 0)
            {
                // 寻找开启列表中的F最小的节点，如果F相同，选取H最小的
                var currentNode = openList[0];

                foreach (var node in openList)
                {
                    if (node == currentNode) continue;

                    var curCost = costDic[currentNode];
                    var cost = costDic[node];

                    if (cost.g + cost.h < curCost.g + curCost.h)
                    {
                        currentNode = node;
                    }
                }

                // 把当前节点从开启列表中移除，并加入到关闭列表中
                openList.Remove(currentNode);
                closeSet.Add(currentNode);

                // 如果是目的节点，返回
                if (currentNode == endNode)
                {
                    List<Vector2Int> path = new List<Vector2Int>();
                    while (currentNode != startNode)
                    {
                        path.Insert(0, currentNode.pos);
                        currentNode = parentDic[currentNode];
                    }
                    return path;
                }

                // 搜索当前节点的所有相邻节点
                var p = currentNode.pos;
                for (int y = p.y - 1; y <= p.y + 1; ++y)
                {
                    for (int x = p.x - 1; x <= p.x + 1; ++x)
                    {
                        var node = Ice.Gameplay.map[x, y];
                        if (node == currentNode) continue;

                        // 如果节点不可通过或者已在关闭列表中，跳出
                        if (!node.IsPath || closeSet.Contains(node))
                        {
                            continue;
                        }
                        var curCost = costDic[currentNode];
                        int gCost = curCost.g + CalDis(currentNode.pos, node.pos);

                        // 如果新路径到相邻点的距离更短 或者不在开启列表中
                        if (gCost < curCost.g || !openList.Contains(node))
                        {
                            // 更新相邻点的F，G，H
                            costDic[node] = (gCost, CalDis(node.pos, endNode.pos));
                            // 设置相邻点的父节点为当前节点
                            parentDic[node] = currentNode;
                            // 如果不在开启列表中，加入到开启列表中
                            if (!openList.Contains(node))
                            {
                                openList.Add(node);
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}