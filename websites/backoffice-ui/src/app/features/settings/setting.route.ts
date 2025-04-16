import {CategoryComponent} from './views/category/category.component';
import {Routes} from '@angular/router';
import {GenreComponent} from './views/genre/genre.component';

export const settingRoutes: Routes = [
  { path: 'category', component: CategoryComponent },
  { path: 'genre', component: GenreComponent }
]

export default settingRoutes;
