import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule, FormBuilder, FormArray } from '@angular/forms';
import { NpvCalculatorComponent } from './npv-calculator.component';
import { Store } from '@ngrx/store';
import { provideMockStore, MockStore } from '@ngrx/store/testing';
import { NpvState } from '../../store/npv/npv.reducer';
import { NpvCashFlowResultComponent } from './npv-cash-flow-result/npv-cash-flow-result.component';

describe('NpvCalculatorComponent', () => {
  let component: NpvCalculatorComponent;
  let fixture: ComponentFixture<NpvCalculatorComponent>;
  let store: MockStore<{ npv: NpvState }>;
  const initialState = {
    npv: {
      npvWithCashFlowResult: null,
      npvRangeResult: null,
    },
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NpvCalculatorComponent, NpvCashFlowResultComponent],
      imports: [ReactiveFormsModule],
      providers: [FormBuilder, provideMockStore({ initialState })],
    }).compileComponents();

    store = TestBed.inject(Store) as MockStore<{ npv: NpvState }>;
    fixture = TestBed.createComponent(NpvCalculatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('addCashFlow', () => {
    it('should add a new cash flow control to the form array', () => {
      const initialLength = component.cashFlows.length;
      component.addCashFlow();
      expect(component.cashFlows.length).toEqual(initialLength + 1);
    });
  });

  describe('removeCashFlow', () => {
    it('should remove the cash flow control at the specified index', () => {
      component.addCashFlow();
      const initialLength = component.cashFlows.length;
      component.removeCashFlow(0);
      expect(component.cashFlows.length).toEqual(initialLength - 1);
    });
  });
});
