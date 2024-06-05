import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { of } from 'rxjs';
import {
  calculateNPVWithCashFlowStream,
  calculateNPVWithCashFlowStreamSuccess,
  calculateNPVWithCashFlowStreamFailure,
  calculateNPVRangeWithCashFlowStream,
  calculateNPVRangeWithCashFlowStreamSuccess,
  calculateNPVRangeWithCashFlowStreamFailure,
} from './npv.actions';
import { NpvService } from '../../features/npv-calculator/services/npv.service';

@Injectable()
export class NpvEffects {
  constructor(private actions$: Actions, private npvService: NpvService) {}

  calculateNPVWithCashFlowStream$ = createEffect(() =>
    this.actions$.pipe(
      ofType(calculateNPVWithCashFlowStream),
      mergeMap((action) =>
        this.npvService.calculateNPVWithCashFlowStream(action.npvRequest).pipe(
          map((result) => calculateNPVWithCashFlowStreamSuccess({ result })),
          catchError((error) =>
            of(calculateNPVWithCashFlowStreamFailure({ error }))
          )
        )
      )
    )
  );

  calculateNPVRangeWithCashFlowStream$ = createEffect(() =>
    this.actions$.pipe(
      ofType(calculateNPVRangeWithCashFlowStream),
      mergeMap((action) =>
        this.npvService
          .calculateNPVRangeWithCashFlowStream(action.npvRequest)
          .pipe(
            map((result) =>
              calculateNPVRangeWithCashFlowStreamSuccess({ result })
            ),
            catchError((error) =>
              of(calculateNPVRangeWithCashFlowStreamFailure({ error }))
            )
          )
      )
    )
  );
}
