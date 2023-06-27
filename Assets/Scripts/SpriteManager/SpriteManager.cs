using System.Collections.Generic;

namespace Assets.Scripts.SpriteManager
{
    using System;

    using Assets.Libs.Config;
    using Assets.Libs.Enum;
    using Assets.Scripts.Base;
    using Assets.Scripts.LogManager;
    using UnityEngine;

    /// <summary>
    /// 图集加载统一管理
    /// </summary>
    public class SpriteManager : Singleton<SpriteManager>
    {
        /// <summary>
        /// 食物植物图集
        /// </summary>
        private readonly List<Sprite> m_PlantSprites = new List<Sprite>(10);

        /// <summary>
        /// 食物冰激凌图集
        /// </summary>
        private readonly List<Sprite> m_IceCreamSprites = new List<Sprite>(9);

        /// <summary>
        /// 小蛇蓝色皮肤图集
        /// </summary>
        private readonly List<Sprite> m_BlueSkinSprites = new List<Sprite>(3);

        /// <summary>
        /// 小蛇蓝色皮肤图集
        /// </summary>
        private readonly List<Sprite> m_YellowSprites = new List<Sprite>(3);

        /// <summary>
        /// 初始化，加载
        /// </summary>
        public void Init()
        {
            //植物图集
            try
            {
                m_PlantSprites.Add(Resources.Load<Sprite>("Sprites/Food/Flower/仙人掌"));
                m_PlantSprites.Add(Resources.Load<Sprite>("Sprites/Food/Flower/向日葵"));
                m_PlantSprites.Add(Resources.Load<Sprite>("Sprites/Food/Flower/山谷百合"));
                m_PlantSprites.Add(Resources.Load<Sprite>("Sprites/Food/Flower/白梅"));
                m_PlantSprites.Add(Resources.Load<Sprite>("Sprites/Food/Flower/紫阳花"));
                m_PlantSprites.Add(Resources.Load<Sprite>("Sprites/Food/Flower/红树"));
                m_PlantSprites.Add(Resources.Load<Sprite>("Sprites/Food/Flower/花"));
                m_PlantSprites.Add(Resources.Load<Sprite>("Sprites/Food/Flower/花朵"));
                m_PlantSprites.Add(Resources.Load<Sprite>("Sprites/Food/Flower/蒲公英"));
                m_PlantSprites.Add(Resources.Load<Sprite>("Sprites/Food/Flower/郁金香"));

                foreach (var plantSprite in this.m_PlantSprites)
                {
                    if (plantSprite is null)
                    {
                        LogManager.instance.LogToFile($"植物图集加载错误{plantSprite.name}", LogLevel.Error);
                    }
                    else
                    {
                        LogManager.instance.LogToFile($"{m_PlantSprites.Count}种植物图集全部加载成功", LogLevel.Information);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

            //冰淇淋图集
            try
            {
                m_IceCreamSprites.Add(Resources.Load<Sprite>("Sprites/Food/iceCream/iceCream-01"));
                m_IceCreamSprites.Add(Resources.Load<Sprite>("Sprites/Food/iceCream/iceCream-02"));
                m_IceCreamSprites.Add(Resources.Load<Sprite>("Sprites/Food/iceCream/iceCream-03"));
                m_IceCreamSprites.Add(Resources.Load<Sprite>("Sprites/Food/iceCream/iceCream-04"));
                m_IceCreamSprites.Add(Resources.Load<Sprite>("Sprites/Food/iceCream/iceCream-05"));
                m_IceCreamSprites.Add(Resources.Load<Sprite>("Sprites/Food/iceCream/iceCream-06"));
                m_IceCreamSprites.Add(Resources.Load<Sprite>("Sprites/Food/iceCream/iceCream-07"));
                m_IceCreamSprites.Add(Resources.Load<Sprite>("Sprites/Food/iceCream/iceCream-08"));
                m_IceCreamSprites.Add(Resources.Load<Sprite>("Sprites/Food/iceCream/iceCream-09"));
                m_IceCreamSprites.Add(Resources.Load<Sprite>("Sprites/Food/iceCream/iceCream-10"));
                foreach (var sprite in this.m_IceCreamSprites)
                {
                    if (sprite is null)
                    {
                        LogManager.instance.LogToFile($"冰淇淋图集加载错误{sprite.name}", LogLevel.Error);
                    }
                    else
                    {
                        LogManager.instance.LogToFile($"{m_IceCreamSprites.Count}种冰淇淋图集全部加载成功", LogLevel.Information);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

            //蓝色皮肤图集
            try
            {
                m_BlueSkinSprites.Add(Resources.Load<Sprite>("Sprites/Snake/sh01"));
                m_BlueSkinSprites.Add(Resources.Load<Sprite>("Sprites/Snake/sb0101"));
                m_BlueSkinSprites.Add(Resources.Load<Sprite>("Sprites/Snake/sb0102"));
                foreach (var sprite in this.m_BlueSkinSprites)
                {
                    if (sprite is null)
                    {
                        LogManager.instance.LogToFile($"小蛇蓝色皮肤图集加载错误{sprite.name}", LogLevel.Error);
                    }
                    else
                    {
                        LogManager.instance.LogToFile($"{m_BlueSkinSprites.Count}种小蛇蓝色皮肤图集全部加载成功", LogLevel.Information);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

            //黄色皮肤图集
            try
            {
                m_YellowSprites.Add(Resources.Load<Sprite>("Sprites/Snake/sh02"));
                m_YellowSprites.Add(Resources.Load<Sprite>("Sprites/Snake/sb0201"));
                m_YellowSprites.Add(Resources.Load<Sprite>("Sprites/Snake/sb0202"));
                foreach (var sprite in this.m_YellowSprites)
                {
                    if (sprite is null)
                    {
                        LogManager.instance.LogToFile($"小蛇黄色皮肤图集加载错误{sprite.name}", LogLevel.Error);
                    }
                    else
                    {
                        LogManager.instance.LogToFile($"{m_YellowSprites.Count}种小蛇黄色皮肤图集全部加载成功", LogLevel.Information);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        /// <summary>
        /// 获取食物图集
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="Sprite"/>.
        /// </returns>
        public Sprite GetFoodSprite(int index)  
        {
            var curFoodSprite = GlobalSetting.instance.curFoodSprite;
            switch (curFoodSprite)
            {
                case FoodSprite.IceCream:
                    return m_IceCreamSprites[index];
                case FoodSprite.Plant:
                    return m_PlantSprites[index];
                default:
                    Debug.LogError($"未知的食物图集{curFoodSprite}");
                    break;
            }

            return null;
        }

        /// <summary>
        /// 获取小蛇皮肤图集
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="Sprite"/>.
        /// </returns>
        public Sprite GetSnakeSkinSprite(int index)     
        {
            var curSnakeSkinSprite = GlobalSetting.instance.curSnakeSprite;
            switch (curSnakeSkinSprite)
            {
                case SnakeSprite.BlueSkin:
                    return m_BlueSkinSprites[index];
                case SnakeSprite.YellowSkin:
                    return m_YellowSprites[index];
                default:
                    Debug.LogError($"未知的食物图集{curSnakeSkinSprite}");
                    break;
            }

            return null;
        }
    }
}
