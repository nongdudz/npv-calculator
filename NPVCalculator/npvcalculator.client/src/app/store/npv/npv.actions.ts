import { createAction, props } from '@ngrx/store';
import { NPVRequest } from '../../features/npv-calculator/models/npv-request.model';
import {
  NPVRangeResponse,
  NPVResponse,
} from '../../features/npv-calculator/models/npv-response.model';

export const calculateNPVWithCashFlowStream = createAction(
  '[NPV] Calculate NPV With Cash Flow Stream',
  props<{ npvRequest: NPVRequest }>()
);

export const calculateNPVWithCashFlowStreamSuccess = createAction(
  '[NPV] Calculate NPV With Cash Flow Stream Success',
  props<{ result: NPVResponse }>()
);

export const calculateNPVWithCashFlowStreamFailure = createAction(
  '[NPV] Calculate NPV With Cash Flow Stream Failure',
  props<{ error: any }>()
);

export const calculateNPVRangeWithCashFlowStream = createAction(
  '[NPV] Calculate NPV Range With Cash Flow Stream',
  props<{ npvRequest: NPVRequest }>()
);

export const calculateNPVRangeWithCashFlowStreamSuccess = createAction(
  '[NPV] Calculate NPV Range With Cash Flow Stream Success',
  props<{ result: NPVRangeResponse[] }>()
);

export const calculateNPVRangeWithCashFlowStreamFailure = createAction(
  '[NPV] Calculate NPV Range With Cash Flow Stream Failure',
  props<{ error: any }>()
);
