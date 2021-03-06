﻿using Blazor.Fluxor;

namespace ReduxDevToolsIntegration.Client.Store.FetchData
{
	public class GetForecastDataFailedActionReducer : Reducer<FetchDataState, GetForecastDataFailedAction>
	{
		public override FetchDataState Reduce(FetchDataState state, GetForecastDataFailedAction action) =>
			new FetchDataState(
				isLoading: false,
				errorMessage: action.ErrorMessage,
				forecasts: null);
	}
}
