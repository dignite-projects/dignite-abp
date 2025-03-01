import { RouteReuseStrategy, ActivatedRouteSnapshot, DetachedRouteHandle } from '@angular/router';
/**
 * 路由复用策略
 * 
 * 1. 实现shouldReuseRoute方法，判断是否同一路由时复用路由
 * 2. 实现retrieve方法，获取存储路由
 * 3. 实现shouldDetach方法，判断是否有路由存储
 * 4. 实现store方法，存储路由
 * 5. 实现shouldAttach方法，判断是否有路由存储
 * 6. 实现retrieve方法，获取存储路由
 * 7. 实现destroy方法，销毁路由
 * 
 * *使用方法
    @NgModule({
        providers: [
            { provide: RouteReuseStrategy, useClass: SimpleReuseStrategy }
        ],
    })

    data: { keep: true }
 */
export class SimpleReuseStrategy implements RouteReuseStrategy {
    static cacheRouters = new Map<string, DetachedRouteHandle>();

    public static deleteRouteCache(url): void {
        if (SimpleReuseStrategy.cacheRouters.has(url)) {
            const handle: any = SimpleReuseStrategy.cacheRouters.get(url);
            try {
                handle.componentRef.destory();
            } catch (e) { }
            SimpleReuseStrategy.cacheRouters.delete(url);
        }
    }

    public static deleteAllRouteCache(): void {
        SimpleReuseStrategy.cacheRouters.forEach((handle: any, key) => {
            SimpleReuseStrategy.deleteRouteCache(key);
        });
    }

    // one 进入路由触发，是否同一路由时复用路由
    shouldReuseRoute(future: ActivatedRouteSnapshot, curr: ActivatedRouteSnapshot): boolean {
        return (
            future.routeConfig === curr.routeConfig &&
            JSON.stringify(future.params) === JSON.stringify(curr.params)
        );
    }

    // 获取存储路由
    retrieve(route: ActivatedRouteSnapshot): DetachedRouteHandle {
        const url = this.getFullRouteURL(route);
        if (route.data.keep && SimpleReuseStrategy.cacheRouters.has(url)) {
            return SimpleReuseStrategy.cacheRouters.get(url);
        } else {
            return null;
        }
    }

    // Whether to allow multiplexing routes
    shouldDetach(route: ActivatedRouteSnapshot): boolean {
        return Boolean(route.data.keep);
    }
    //It is triggered when the route leaves. The route is stored
    store(route: ActivatedRouteSnapshot, handle: DetachedRouteHandle): void {
        const url = this.getFullRouteURL(route);
        // 先把之前缓存的删除,
        SimpleReuseStrategy.cacheRouters.forEach((handle: any, key) => {
            SimpleReuseStrategy.deleteRouteCache(key);
        });
        SimpleReuseStrategy.cacheRouters.set(url, handle);
    }
    //  Whether to allow route restoration
    shouldAttach(route: ActivatedRouteSnapshot): boolean {
        const url = this.getFullRouteURL(route);
        return Boolean(route.data.keep) && SimpleReuseStrategy.cacheRouters.has(url);
    }

    // Gets the current route url
    private getFullRouteURL(route: ActivatedRouteSnapshot): string {
        const { pathFromRoot } = route;
        let fullRouteUrlPath: string[] = [];
        pathFromRoot.forEach((item: ActivatedRouteSnapshot) => {
            fullRouteUrlPath = fullRouteUrlPath.concat(this.getRouteUrlPath(item));
        });
        return `/${fullRouteUrlPath.join('/')}`;
    }
    private getRouteUrlPath(route: ActivatedRouteSnapshot) {
        return route.url.map(urlSegment => urlSegment.path);
    }
}
