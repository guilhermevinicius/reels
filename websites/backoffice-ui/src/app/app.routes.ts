import { Routes } from '@angular/router';
import { MainLayoutComponent } from './features/main/views/main-layout/main-layout.component';

export const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    loadChildren: () => import('./features/main/main.route')
  }
];
