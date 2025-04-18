import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {Component} from '@angular/core';
import {ICategory} from '../../models';
import {CategoryService} from '../../services';

@Component({
  selector: 'settings-category-component',
  templateUrl: './category.component.html',
  imports: [
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    CategoryService
  ]
})
export class CategoryComponent {
  showDialogDelete: boolean = false
  showForm: boolean = false
  categories: ICategory[] = []
  category: ICategory | null = null

  categoryForm = new FormGroup({
    name: new FormControl<string | null>(null, [Validators.required]),
    description: new FormControl<string | null>(null, [Validators.required]),
  })

  constructor(
    private service: CategoryService,
  ) {
    this.listCategory()
  }

  getCategoryAndSetForm(id: string): void {
    this.service.getCategory$(id).subscribe({
      next: data => {
        this.category = data.data
        this.categoryForm.setValue({
          name: data.data.name,
          description: data.data.description
        })
      },
      error: error => {
      }
    })
  }

  listCategory() {
    this.service.listCategory$().subscribe({
      next: data => {
        this.categories = data.data
      },
      error: error => {

      }
    })
  }

  openDialogDeleteDialog($event: Event, categoryId: string): void {
    this.showDialogDelete = true
    // this.confirmationService.confirm({
    //   target: $event.target as EventTarget,
    //   message: 'Are you sure you want to delete this category?',
    //   header: 'Confirmation',
    //   closable: true,
    //   closeOnEscape: true,
    //   rejectButtonProps: {
    //     label: 'Cancel',
    //     severity: 'secondary',
    //     outlined: true,
    //   },
    //   acceptButtonProps: {
    //     label: 'Save',
    //   },
    //   accept: () => {
    //     this.service.deleteCategory$(categoryId).subscribe({
    //       next: data => {
    //         this.messageService.add({
    //           severity: 'success',
    //           summary: 'Tudo certo'
    //         })
    //         this.listCategory()
    //       },
    //       error: error => {
    //         this.messageService.add({
    //           summary: 'Algo deu errado',
    //           severity: 'warning',
    //         })
    //       }
    //     })
    //   },
    // })
  }

  openForm(id?: string): void {
    this.showForm = true
    if (id)
      this.getCategoryAndSetForm(id)
  }

  closeForm(): void {
    this.showForm = false
    this.categoryForm.reset();
  }

  onSubmitForm() {
    if (this.category) {
      this.updateCategory()
    } else {
      this.createCategory()
    }
  }

  createCategory() {
    this.service.createCategory$({
      name: this.categoryForm.value.name!,
      description: this.categoryForm.value.description!
    }).subscribe({
      next: data => {
        this.categoryForm.reset()
        this.showForm = false
        this.listCategory()
      },
      error: error => {
      }
    })
  }

  updateCategory() {
    this.service.updateCategory$({
      id: this.category?.id!,
      name: this.categoryForm.value.name!,
      description: this.categoryForm.value.description!
    }).subscribe({
      next: data => {
        this.categoryForm.reset()
        this.showForm = false
        this.listCategory()
        this.category = null
      },
      error: error => {
      }
    })
  }

}
