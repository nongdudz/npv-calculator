import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NpvCalculatorComponent } from './features/npv-calculator/npv-calculator.component';
import { NpvService } from './features/npv-calculator/services/npv.service';
import { ReactiveFormsModule } from '@angular/forms';
import { NpvCashFlowResultComponent } from './features/npv-calculator/npv-cash-flow-result/npv-cash-flow-result.component';
import { npvReducer } from './store/npv/npv.reducer';
import { NpvEffects } from './store/npv/npv.effects';

@NgModule({
  declarations: [
    AppComponent,
    NpvCalculatorComponent,
    NpvCashFlowResultComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    StoreModule.forRoot({ npv: npvReducer }),
    EffectsModule.forRoot([NpvEffects]),
    StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: false }),
  ],
  providers: [NpvService],
  bootstrap: [AppComponent],
})
export class AppModule {}
