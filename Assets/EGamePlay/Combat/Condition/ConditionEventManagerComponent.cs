﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 条件事件管理器，在这里管理一个战斗实体所有条件达成事件的添加监听、移除监听、触发流程
    /// </summary>
    public sealed class ConditionEventManagerComponent : Component
    {
        private Dictionary<Action, ConditionEntity> Conditions { get; set; } = new Dictionary<Action, ConditionEntity>();


        public void Initialize()
        {
        }

        public void AddListener(ConditionType conditionType, Action action, object paramObj = null)
        {
            switch (conditionType)
            {
                case ConditionType.WhenInTimeNoDamage:
                    var time = (float)paramObj;
                    var condition = EntityFactory.CreateWithParent<NoDamageTimeCondition>(Entity, time);
                    Conditions.Add(action, condition);
                    condition.StartListen(action);
                    break;
                case ConditionType.WhenHPLower:
                    break;
                case ConditionType.WhenHPPctLower:
                    break;
                default:
                    break;
            }
        }



        public void RemoveListener(ActionPointType actionPointType, Action<CombatAction> action)
        {
            //if (Conditions.ContainsKey(actionPointType))
            //{
            //    Conditions[actionPointType].Listeners.Remove(action);
            //}
        }

        public void TriggerActionPoint(ActionPointType actionPointType, CombatAction action)
        {
            //if (Conditions.ContainsKey(actionPointType) && Conditions[actionPointType].Listeners.Count > 0)
            //{
            //    for (int i = Conditions[actionPointType].Listeners.Count - 1; i >= 0; i--)
            //    {
            //        var item = Conditions[actionPointType].Listeners[i];
            //        item.Invoke(action);
            //    }
            //}
        }
    }
}