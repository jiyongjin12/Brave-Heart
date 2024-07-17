using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Map
{
    public class MapPlayerTracker : MonoBehaviour
    {
        public bool lockAfterSelecting = false;
        public float enterNodeDelay = 1f;
        public MapManager mapManager;
        public MapView view;

        public static MapPlayerTracker Instance;

        public bool Locked { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        public void SelectNode(MapNode mapNode)
        {
            if (Locked) return;

            // Debug.Log("Selected node: " + mapNode.Node.point);

            if (mapManager.CurrentMap.path.Count == 0)
            {
                // player has not selected the node yet, he can select any of the nodes with y = 0
                if (mapNode.Node.point.y == 0)
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed();
            }
            else
            {
                var currentPoint = mapManager.CurrentMap.path[mapManager.CurrentMap.path.Count - 1];
                var currentNode = mapManager.CurrentMap.GetNode(currentPoint);

                if (currentNode != null && currentNode.outgoing.Any(point => point.Equals(mapNode.Node.point)))
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed();
            }
        }

        private void SendPlayerToNode(MapNode mapNode)
        {
            Locked = lockAfterSelecting;
            mapManager.CurrentMap.path.Add(mapNode.Node.point);
            mapManager.SaveMap();
            view.SetAttainableNodes();
            view.SetLineColors();
            mapNode.ShowSwirlAnimation();

            DOTween.Sequence().AppendInterval(enterNodeDelay).OnComplete(() => EnterNode(mapNode));
        }

        private static void Test()
        {
            Debug.Log("´ÙÀ½ ¾À");
            GameSuvManager.instance.gameData.stageNum = 1;
            FadeSystem.instance.ChangeScene("SampleScene");
        }

        private static void EnterNode(MapNode mapNode)
        {
            switch (mapNode.Node.nodeType)
            {
                case NodeType.MinorEnemy:
                    Debug.Log("ÀÏ¹Ý¸÷");
                    Test();
                    break;
                case NodeType.EliteEnemy:
                    Debug.Log("¿¤¸®Æ®¸÷");
                    GameSuvManager.instance.gameData.stageNum = 2;
                    FadeSystem.instance.ChangeScene("EliteScene");
                    break;
                case NodeType.RestSite:
                    Debug.Log("½°ÅÍ");
                    GameSuvManager.instance.gameData.stageNum = 3;
                    FadeSystem.instance.ChangeScene("RecoveryScene");
                    break;
                case NodeType.Treasure:
                    Debug.Log("»óÀÚ");
                    GameSuvManager.instance.gameData.stageNum = 4;
                    break;
                case NodeType.Store:
                    Debug.Log("»óÁ¡");
                    GameSuvManager.instance.gameData.stageNum = 5;
                    break;
                case NodeType.Boss:
                    Debug.Log("º¸½º");
                    GameSuvManager.instance.gameData.stageNum = 6;
                    FadeSystem.instance.ChangeScene("BossScene");
                    break;
                case NodeType.Mystery:
                    Debug.Log("¹°À½Ç¥¹æ");
                    GameSuvManager.instance.gameData.stageNum = 10;
                    FadeSystem.instance.ChangeIntScene(GameSuvManager.instance.Rand());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PlayWarningThatNodeCannotBeAccessed()
        {
            Debug.Log("¸ø°¡ ÀÌ´®¾Æ");
        }
    }

}