import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, GuardsCheckEnd } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { StepsComponent } from './steps/steps.component';
import { Gs8Component } from './gs8/gs8.component';
import { FooterComponent } from './footer/footer.component';
import { DiplomaAppComponent } from './diplomaApp/diplomaApp.component';
import { AuthGuard } from './guards/auth-gaurd.service';
import { LoginComponent } from './login/login.component';
import { JwtHelper } from 'angular2-jwt';
import { RegisterComponent } from './register/register.component';
import { NavService } from './services/navService.service';
import { ScheduleExamComponent } from './schedFinal/schedFinal.component';
import { CelebrateComponent } from './celebrate/celebrate.component';
import { DoctorateSurveyComponent } from './docSurvey/docSurvey.component';
import { ElectronicSubmitComponent } from './elecSubmit/elecSubmit.component';
import { EtdInfoComponent } from './EtdInfo/etdInfo.component';
import { ThesisComponent } from './thesis/thesis.component';
import { FinalReportComponent } from './finalReport/finalReport.component';
import { FinalVisitComponent } from './finalVisit/finalVisit.component';
import { CopyrightComponent } from './copyright/copyright.component';
import { ProQuestComponent } from './proQuestFee/proQuest.component';
import { GraduationFeeComponent } from './graduationFee/graduationFee.component';
import { AccountComponent } from './account/account.component';
import { AdminComponent } from './admin/admin.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    StepsComponent,
    Gs8Component,
    FooterComponent,
    DiplomaAppComponent,
    LoginComponent,
    RegisterComponent,
    ScheduleExamComponent,
    CelebrateComponent,
    DoctorateSurveyComponent,
    ElectronicSubmitComponent,
    EtdInfoComponent,
    ThesisComponent,
    FinalReportComponent,
    FinalVisitComponent,
    CopyrightComponent,
    ProQuestComponent,
    GraduationFeeComponent,
    AccountComponent,
    AdminComponent
    
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'account', component: AccountComponent, canActivate: [AuthGuard] },
      { path: 'counter', component: CounterComponent, canActivate: [AuthGuard] },
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthGuard] },
      { path: 'steps', component: StepsComponent, canActivate: [AuthGuard] },
      { path: 'footer', component: FooterComponent, canActivate: [AuthGuard] },      
      { path: 'GS8', component: Gs8Component, canActivate: [AuthGuard] },
      { path: 'Survey of Earned Doctorates', component: DoctorateSurveyComponent, canActivate: [AuthGuard] },
      { path: 'Diploma App', component: DiplomaAppComponent, canActivate: [AuthGuard] },
      { path: 'ETD Info', component: EtdInfoComponent, canActivate: [AuthGuard] },
      { path: 'Submit Thesis-Dissertation', component: ElectronicSubmitComponent, canActivate: [AuthGuard] },
      { path: 'Thesis-Disseration and Exam', component: ThesisComponent, canActivate: [AuthGuard] },
      { path: 'Report of Final Exam', component: FinalReportComponent, canActivate: [AuthGuard] },
      { path: 'Final Visit', component: FinalVisitComponent, canActivate: [AuthGuard] },
      { path: 'Graduation Fee', component: GraduationFeeComponent, canActivate: [AuthGuard] },
      { path: 'ProQuest Fee', component: ProQuestComponent, canActivate: [AuthGuard] },
      { path: 'Schedule Exam', component: ScheduleExamComponent, canActivate: [AuthGuard] },
      { path: 'Publishing and Copyright', component: CopyrightComponent, canActivate: [AuthGuard] },
      { path: 'Completion', component: CelebrateComponent, canActivate: [AuthGuard] },
      { path: 'admin', component: AdminComponent, canActivate: [AuthGuard] },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },

    ])
  ],
  providers: [JwtHelper, AuthGuard, NavService],
  bootstrap: [AppComponent]
})
export class AppModule { }
