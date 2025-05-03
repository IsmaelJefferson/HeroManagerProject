import { Routes } from '@angular/router';
import { HeroListComponent } from './Features/Hero/list-heroes/list-heroes.component';
import { HeroAddComponent } from './Features/Hero/create-hero/create-hero.component';
import { HeroEditComponent } from './Features/Hero/edit-hero/edit-hero.component';
import { HeroDetailComponent } from './Features/Hero/hero-details/hero-details.component';

export const routes: Routes = [
  { path: '', component: HeroListComponent },
  { path: 'add', component: HeroAddComponent },
  { path: 'edit/:id', component: HeroEditComponent },
  { path: 'detail/:id', component: HeroDetailComponent },
];