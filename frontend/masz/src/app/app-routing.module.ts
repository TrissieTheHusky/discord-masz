import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageNotFoundComponent } from './components/defaults/page-not-found/page-not-found.component';
import { GuildAddComponent } from './components/guild/guild-add/guild-add.component';
import { GuildListComponent } from './components/guild/guild-list/guild-list.component';
import { GuildOverviewComponent } from './components/guild/guild-overview/guild-overview.component';
import { GuildPatchComponent } from './components/guild/guild-patch/guild-patch.component';
import { PatchnotesComponent } from './components/information/patchnotes/patchnotes.component';
import { CaseViewComponent } from './components/modcase/case-view/case-view.component';
import { AuthGuard } from './guards/auth.guard';
import { NonAuthGuard } from './guards/non-auth.guard';
import { IndexComponent } from './index/index.component';

const routes: Routes = [
  { path: 'guilds', component: GuildListComponent, canActivate: [AuthGuard] },
  { path: 'guilds/:guildid/edit', component: GuildPatchComponent, canActivate: [AuthGuard] },
  { path: 'guilds/new', component: GuildAddComponent, canActivate: [AuthGuard] },
  { path: 'guilds/:guildid', component: GuildOverviewComponent, canActivate: [AuthGuard] },
  { path: 'guilds/:guildid/cases/:caseid', component: CaseViewComponent, canActivate: [AuthGuard] },
  { path: 'patchnotes',  component: PatchnotesComponent, canActivate: [AuthGuard] },
  { path: 'login',  component: IndexComponent, pathMatch: 'full', canActivate: [NonAuthGuard] },
  { path: '',  redirectTo: 'login', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
