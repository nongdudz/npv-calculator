import { createReducer, on } from '@ngrx/store';
import {
  calculateNPVRangeWithCashFlowStreamSuccess,
  calculateNPVWithCashFlowStreamSuccess,
} from './npv.actions';
import {
  NPVRangeResponse,
  NPVResponse,
} from '../../features/npv-calculator/models/npv-response.model';

export interface NpvState {
  npvWithCashFlowResult: NPVResponse | null;
  npvRangeResult: NPVRangeResponse[] | null;
}

export const initialState: NpvState = {
  npvWithCashFlowResult: null,
  npvRangeResult: null,
};

export const npvReducer = createReducer(
  initialState,
  on(calculateNPVWithCashFlowStreamSuccess, (state, { result }) => ({
    ...state,
    npvWithCashFlowResult: result,
  })),
  on(calculateNPVRangeWithCashFlowStreamSuccess, (state, { result }) => ({
    ...state,
    npvRangeResult: result,
  }))
);
