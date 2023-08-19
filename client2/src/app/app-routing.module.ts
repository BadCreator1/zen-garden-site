import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'news', loadChildren: () => import('./news/news.module').then(mod => mod.NewsModule), data: { breadcrumb: 'News' } },
  { path: '', loadChildren: () => import('./home/home.module').then(mod => mod.HomeModule), data: { breadcrumb: 'Home' } },

  { path: 'account', loadChildren: () => import('./account/account.module').then(mod => mod.AccountModule), data: { breadcrumb: 'Account' } },
  { path: '**', redirectTo: 'not-found', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
