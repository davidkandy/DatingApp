import { Injectable } from '@angular/core';
import { CanDeactivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  canDeactivate(
    component: MemberEditComponent ):boolean {
      // This makes sure that when the user is editing his/her profile, and it mistakenly clicks on a link, it doesn't lose all the progress and it gives it a warning 
      if(component.editForm.dirty){
        return confirm('Are you sure you want to continue? Any unsaved changes will be lost');
      }
    return true;
  }
  
}
