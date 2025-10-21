import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'orderByDate',
  standalone: true
})
export class OrderByDatePipe implements PipeTransform {
  transform(items: any[]): any[] {
    if (!items) return [];
    // 🔁 Inverser le tri → plus récent en premier
    return items.sort((a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime());
  }
}
