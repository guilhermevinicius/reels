import {Component} from '@angular/core';
import {TableModule} from 'primeng/table';
import {ICategory} from '../../models';
import {Button} from 'primeng/button';
import {CategoryService} from '../../services';
import {Dialog} from 'primeng/dialog';
import {ConfirmDialog} from 'primeng/confirmdialog';
import {ConfirmationService, MessageService} from 'primeng/api';
import {FloatLabel} from 'primeng/floatlabel';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {Toast} from 'primeng/toast';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'settings-category-component',
  templateUrl: './category.component.html',
  imports: [
    TableModule,
    Button,
    Dialog,
    ConfirmDialog,
    FloatLabel,
    FormsModule,
    ReactiveFormsModule,
    Toast,
    RouterLink
  ],
  providers: [
    CategoryService,
    ConfirmationService,
    MessageService
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
    private confirmationService: ConfirmationService,
    private messageService: MessageService
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
        this.messageService.add({
          summary: 'Error',
          severity: 'warning',
        })
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
    this.confirmationService.confirm({
      target: $event.target as EventTarget,
      message: 'Are you sure you want to delete this category?',
      header: 'Confirmation',
      closable: true,
      closeOnEscape: true,
      rejectButtonProps: {
        label: 'Cancel',
        severity: 'secondary',
        outlined: true,
      },
      acceptButtonProps: {
        label: 'Save',
      },
      accept: () => {
        this.service.deleteCategory$(categoryId).subscribe({
          next: data => {
            this.messageService.add({
              severity: 'success',
              summary: 'Tudo certo'
            })
            this.listCategory()
          },
          error: error => {
            this.messageService.add({
              summary: 'Algo deu errado',
              severity: 'warning',
            })
          }
        })
      },
    })
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
        this.messageService.add({
          text: 'Error ao criar category',
        })
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
        this.category = null,
          this.messageService.add({
            severity: 'success',
            summary: 'Tudo certo!',
            detail: 'Categoria'
          })
      },
      error: error => {
        this.messageService.add({
          text: 'Error ao atualizar category',
        })
      }
    })
  }

}
