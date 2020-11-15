﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EGamePlay.Combat.Ability
{
    public enum PassiveAbilityExcutionType
    {

    }

    /// <summary>
    /// 能力实体，存储着某个英雄某个能力的数据和状态
    /// </summary>
    public class AbilityEntity : Entity
    {
        public CombatEntity AbilityOwner { get; set; }
        public SkillConfigObject SkillConfigObject { get; set; }


        public override void Awake(object paramObject)
        {
            SkillConfigObject = paramObject as SkillConfigObject;
            this.AbilityOwner = Parent as CombatEntity;
            if (SkillConfigObject.SkillSpellType == SkillSpellType.Passive)
            {
                TryActivateAbility();
            }
        }

        public void TryActivateAbility()
        {
            Log.Debug($"{GetType().Name} TryActivateAbility");
            ActivateAbility();
        }
        
        public virtual void ActivateAbility()
        {
            
        }

        public virtual void EndAbility()
        {

        }

        public virtual AbilityExecution CreateAbilityExecution()
        {
            return null;
        }
        
        public virtual void ApplyAbilityEffect(CombatEntity targetEntity)
        {
            foreach (var item in SkillConfigObject.Effects)
            {
                if (item is DamageEffect damageEffect)
                {
                    var operation = CombatActionManager.CreateAction<DamageAction>(this.AbilityOwner);
                    operation.Target = targetEntity;
                    operation.DamageSource = DamageSource.Skill;
                    operation.DamageEffect = damageEffect;
                    operation.ApplyDamage();
                }
                else if (item is CureEffect cureEffect)
                {
                    var operation = CombatActionManager.CreateAction<CureAction>(this.AbilityOwner);
                    operation.Target = targetEntity;
                    operation.CureEffect = cureEffect;
                    operation.ApplyCure();
                }
                else
                {
                    var operation = CombatActionManager.CreateAction<AssignEffectAction>(this.AbilityOwner);
                    operation.Target = targetEntity;
                    operation.Effect = item;
                    if (item is AddStatusEffect addStatusEffect)
                    {
                        addStatusEffect.AddStatus.Duration = addStatusEffect.Duration;
                    }
                    operation.ApplyAssignEffect();
                }
            }
        }
    }
}