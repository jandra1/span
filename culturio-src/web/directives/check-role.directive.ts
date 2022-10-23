import { Directive ,ElementRef,Input,Renderer2,SimpleChange,TemplateRef,ViewContainerRef } from '@angular/core';
import { UserService } from '../users/services/user.service';

@Directive({
  selector: '[appCheckRole]'
})
export class CheckRoleDirective {

      @Input("roleType") requiredRole: string[];
      isVisible = false;
      currentUserRole:string;

      constructor(
        private renderer: Renderer2,
        private elRef: ElementRef<any>,
        private userService: UserService) { }


      ngOnInit() {
        this.getCurrentUserRole();
      }

      ngOnChanges(changes: SimpleChange): void {
        this.validateAccess();
      }

      getCurrentUserRole() {
        this.userService.getUserInfo();
        this.currentUserRole =this.userService.user.role;
      }
      
      private validateAccess(): void {
        if (this.requiredRole && this.requiredRole.includes(this.currentUserRole)) {
          this.renderer.setAttribute(this.elRef.nativeElement, 'disabled', 'true');
          this.renderer.setStyle(this.elRef.nativeElement, 'cursor', 'not-allowed');
        }
      }









    // @Input() set checkRole(role: boolean) {
    //   if (!condition && !this.hasView) {
    //     this.viewContainer.createEmbeddedView(this.templateRef);
    //     this.hasView = true;
    //   } else if (condition && this.hasView) {
    //     this.viewContainer.clear();
    //     this.hasView = false;
    //   }
    // }
}
