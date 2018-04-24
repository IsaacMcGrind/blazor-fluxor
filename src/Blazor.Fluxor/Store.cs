﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Fluxor
{
	public class Store : IStore
	{
		private readonly List<IFeature> Features = new List<IFeature>();
		private readonly Dictionary<Type, List<IEffect>> EffectsByActionType = new Dictionary<Type, List<IEffect>>();

		public void AddFeature(IFeature feature) => Features.Add(feature ?? throw new ArgumentNullException(nameof(feature)));

		public async Task Dispatch(IAction action)
		{
			if (action == null)
				throw new ArgumentNullException(nameof(action));

			Console.WriteLine("Dispatching action: " + action.GetType().Name);

			var actionsToDispatch = new Queue<IAction>();
			actionsToDispatch.Enqueue(action);

			while (actionsToDispatch.Any())
			{
				IAction currentActionToDispatch = actionsToDispatch.Dequeue();
				// Notify all features of this action
				Features.ForEach(x => x.ReceiveDispatchNotificationFromStore(currentActionToDispatch));

				// Get any actions generated by side-effects
				IEnumerable<IAction> actionsCreatedBySideEffects = await TriggerEffects(currentActionToDispatch);
				foreach (IAction actionFromSideEffect in actionsCreatedBySideEffects)
					actionsToDispatch.Enqueue(actionFromSideEffect);
			}
		}

		public void AddEffect(Type actionType, IEffect effect)
		{
			if (actionType == null)
				throw new ArgumentNullException(nameof(actionType));
			if (effect == null)
				throw new ArgumentNullException(nameof(effect));

			Type genericType = typeof(IEffect<>).MakeGenericType(actionType);
			if (!genericType.IsAssignableFrom(effect.GetType()))
				throw new ArgumentException($"Effect {effect.GetType().Name} does not implement IEffect<{actionType.Name}>");

			List<IEffect> effects = GetEffectsForActionType(actionType, true);
			effects.Add(effect);
		}

		private List<IEffect> GetEffectsForActionType(Type actionType, bool createIfNonExistent)
		{
			EffectsByActionType.TryGetValue(actionType, out List<IEffect> effects);
			if (createIfNonExistent && effects == null)
			{
				effects = new List<IEffect>();
				EffectsByActionType[actionType] = effects;
			}
			return effects;
		}

		private async Task<IEnumerable<IAction>> TriggerEffects(IAction action)
		{
			var actionsCreatedBySideEffects = new List<IAction>();

			IEnumerable<IEffect> effectsForAction = GetEffectsForActionType(action.GetType(), false);
			if (effectsForAction != null && effectsForAction.Any())
			{
				Console.WriteLine(effectsForAction.Count() + " effects registered");
				foreach (var effect in effectsForAction)
				{
					Console.WriteLine("Executing effect " + effect.GetType().Name);
					IAction actionFromSideEffect = await effect.Handle(action);
					Console.WriteLine("Name of action returned from effect: " + actionFromSideEffect.GetType().Name);
					if (actionFromSideEffect != null)
						actionsCreatedBySideEffects.Add(actionFromSideEffect);
				}
			}

			return actionsCreatedBySideEffects;
		}

	}
}
