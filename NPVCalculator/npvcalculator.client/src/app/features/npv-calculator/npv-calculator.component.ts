import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { NPVRangeResponse, NPVResponse } from './models/npv-response.model';
import { NPVRequest } from './models/npv-request.model';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { NpvState } from '../../store/npv/npv.reducer';
import {
  calculateNPVRangeWithCashFlowStream,
  calculateNPVWithCashFlowStream,
} from '../../store/npv/npv.actions';
@Component({
  selector: 'app-npv-calculator',
  templateUrl: './npv-calculator.component.html',
  styleUrl: './npv-calculator.component.css',
})
export class NpvCalculatorComponent {
  npvForm: FormGroup;
  npvWithCashFlowResult$: Observable<NPVResponse | null>;
  npvRangeResult$: Observable<NPVRangeResponse[] | null>;

  constructor(
    private fb: FormBuilder,
    private store: Store<{ npv: NpvState }>
  ) {
    this.npvForm = this.fb.group({
      initialInvestments: [0, Validators.required],
      interestRate: [0, Validators.required],
      cashFlows: this.fb.array([this.fb.control(0)]),
      discountRateRange: this.fb.group({
        lowerDiscountRate: [null],
        upperDiscountRate: [null],
        discountRateIncrement: [null],
      }),
    });

    this.npvWithCashFlowResult$ = this.store.select(
      (state) => state.npv.npvWithCashFlowResult
    );
    this.npvRangeResult$ = this.store.select(
      (state) => state.npv.npvRangeResult
    );
  }

  get cashFlows() {
    return this.npvForm.get('cashFlows') as FormArray;
  }

  addCashFlow() {
    this.cashFlows.push(this.fb.control(0));
  }

  removeCashFlow(index: number) {
    this.cashFlows.removeAt(index);
  }

  calculateNPVWithCashFlowStream() {
    const npvFormValue = this.npvForm.value;
    const npvRequest: NPVRequest = {
      initialInvestments: npvFormValue.initialInvestments,
      interestRate: npvFormValue.interestRate,
      cashFlows: npvFormValue.cashFlows,
    };
    this.store.dispatch(calculateNPVWithCashFlowStream({ npvRequest }));
  }

  calculateNPVRangeWithCashFlowStream() {
    const npvRequest: NPVRequest = this.npvForm.value;
    this.store.dispatch(calculateNPVRangeWithCashFlowStream({ npvRequest }));
  }
}
