import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { StepsComponent } from './steps/steps.component';
import { Gs8Component } from './gs8/gs8.component';
import { FooterComponent } from './footer/footer.component';
import { DiplomaAppComponent } from './diplomaApp/diplomaApp.component';


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
    
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'steps', component: StepsComponent },
      { path: 'footer', component: FooterComponent },      
      { path: 'GS8', component: Gs8Component },
      { path: 'Survey of Earned Doctorates', component: Gs8Component },
      { path: 'Diploma App', component: DiplomaAppComponent },
      { path: 'ETD Info', component: Gs8Component },
      { path: 'Submit Thesis-Dissertation', component: Gs8Component },
      { path: 'Thesis-Disseration and Exam', component: Gs8Component },
      { path: 'Report of Final Exam', component: Gs8Component },
      { path: 'Final Visit', component: Gs8Component },
      { path: 'Graduation Fee', component: Gs8Component },
      { path: 'ProQuest Fee', component: Gs8Component },
      { path: 'Schedule Exam', component: Gs8Component },
      { path: 'Publishing and Copyright', component: Gs8Component },
      { path: 'Completion', component: Gs8Component },
      

    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
