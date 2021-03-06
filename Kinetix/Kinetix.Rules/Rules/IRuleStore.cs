﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinetix.Rules
{

    /// <summary>
    /// This interface defines the storage of workflow.
    /// </summary>
    /// 
    public interface IRuleStore
    {

        /// <summary>
        /// Add a rule.
        /// </summary>
        /// <param name="idActivityDefinition">idActivityDefinition.</param>
        /// <param name="ruleDefinition">ruleDefinition.</param>
        void AddRule(RuleDefinition ruleDefinition);

        /// <summary>
        /// Find rules.
        /// </summary>
        /// <param name="itemId">itemId.</param>
        /// <returns>a list of all the rules defined for the itemId</returns>
        IList<RuleDefinition> FindRulesByItemId(long itemId);

        /// <summary>
        /// Removes a rule.
        /// </summary>
        /// <param name="ruleDefinition">ruleDefinition.</param>
        void RemoveRule(RuleDefinition ruleDefinition);

        /// <summary>
        /// Update a rule.
        /// </summary>
        /// <param name="ruleDefinition">ruleDefinition.</param>
        void UpdateRule(RuleDefinition ruleDefinition);

        /// <summary>
        /// Add a condition.
        /// </summary>
        /// <param name="ruleConditionDefinition">ruleConditionDefinition.</param>
        void AddCondition(RuleConditionDefinition ruleConditionDefinition);

        /// <summary>
        /// Remove a condition.
        /// </summary>
        /// <param name="ruleConditionDefinition">ruleConditionDefinition.</param>
        void RemoveCondition(RuleConditionDefinition ruleConditionDefinition);

        /// <summary>
        /// Update a condition.
        /// </summary>
        /// <param name="ruleConditionDefinition">ruleConditionDefinition.</param>
        void UpdateCondition(RuleConditionDefinition ruleConditionDefinition);

        /// <summary>
        /// Find all conditions for a specified rule Id.
        /// </summary>
        /// <param name="ruleId">ruleId.</param>
        /// <returns>a list of all the rules defined for the itemId</returns>
        IList<RuleConditionDefinition> FindConditionByRuleId(long ruleId);

        /// <summary>
        /// Add a selector.
        /// </summary>
        /// <param name="selectorDefinition">selectorDefinition.</param>
        void AddSelector(SelectorDefinition selectorDefinition);

        /// <summary>
        /// Find all selectors for an item Id.
        /// </summary>
        /// <param name="itemId">itemId.</param>
        /// <returns>a list of all the selectors defined for the itemId</returns>
        IList<SelectorDefinition> FindSelectorsByItemId(long itemId);

        /// <summary>
        /// Remove a Selector.
        /// </summary>
        /// <param name="selectorDefinition">selectorDefinition.</param>
        void RemoveSelector(SelectorDefinition selectorDefinition);

        /// <summary>
        /// Update a Selector.
        /// </summary>
        /// <param name="selectorDefinition">selectorDefinition.</param>
        void UpdateSelector(SelectorDefinition selectorDefinition);

        /// <summary>
        /// Add a filter.
        /// </summary>
        /// <param name="ruleFilterDefinition">ruleFilterDefinition.</param>
        void AddFilter(RuleFilterDefinition ruleFilterDefinition);

        /// <summary>
        /// Remove a filter.
        /// </summary>
        /// <param name="ruleFilterDefinition">ruleFilterDefinition.</param>
        void RemoveFilter(RuleFilterDefinition ruleFilterDefinition);

        /// <summary>
        /// Find the filters associated to a selector id.
        /// </summary>
        /// <param name="selectorId">selectorId.</param>
        /// <returns>a list of all the filters defined for the selectorId</returns>
        IList<RuleFilterDefinition> FindFiltersBySelectorId(long selectorId);

        /// <summary>
        /// Update a filter.
        /// </summary>
        /// <param name="ruleFilterDefinition">ruleFilterDefinition.</param>
        void UpdateFilter(RuleFilterDefinition ruleFilterDefinition);
    }
}
