using System.Collections.Generic;
using Interfaces;

namespace Modules
{
    public class ModuleMgr
    {
        public static ModuleMgr Ins = new ModuleMgr();
        private List<IModule> _mods = new List<IModule>();
        
        public void AddModule(IModule mod)
        {
            _mods.Add(mod);
        }
        
        public void RemoveModule(IModule mod)
        {
            _mods.Remove(mod);
            mod.Dispose();
        }
        
        public void Start()
        {
            for (int i = 0, imax = _mods.Count; i < imax; ++i)
            {
                _mods[i].Start();
            }
        }

        public void Update()
        {
            IModule mod;
            for (int i = 0, imax = _mods.Count; i < imax; ++i)
            {
                mod = _mods[i];
                mod.Update();
            }
        }

        public void LateUpdate()
        {
            IModule mod;
            for (int i = 0, imax = _mods.Count; i < imax; ++i)
            {
                mod = _mods[i];
                mod.LateUpdate();
            }
        }

        public void FixedUpdate()
        {
            IModule mod;
            for (int i = 0, imax = _mods.Count; i < imax; ++i)
            {
                mod = _mods[i];
                mod.FixedUpdate();
            }
        }

        public void Pause()
        {
            for (int i = 0, max = _mods.Count; i < max; i++)
            {
                _mods[i].Pause();
            }
        }

        public void Continue()
        {
            for (int i = 0, max = _mods.Count; i < max; i++)
            {
                _mods[i].Continue();
            }
        }

        public void Dispose()
        {
            for (int i = _mods.Count - 1; i >= 0; i--)
            {
                _mods[i].Dispose();
            }
        }
    }
}
