using AutoMapper;
using SuperStore.Data;
using SuperStore.Data.Models;
using SuperStoreAPI.Interfaces;
using SuperStoreAPI.Views;
using System.Collections.Generic;

namespace SuperStoreAPI.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _MemberRepository;
        private readonly IMapper _mapper;

        public MemberService(IMemberRepository MemberRepository, IMapper mapper)
        {
            _MemberRepository = MemberRepository;
            _mapper = mapper;
        }

        public async Task<List<MemberView>> GetMembers()
        {
            var Member = await _MemberRepository.GetMembers();
            return _mapper.Map<List<MemberView>>(Member);
        }

        public async Task<MemberView> GetMember(string UserName)
        {
            var Member = await _MemberRepository.GetMember(UserName);
            if (Member == null)
            {
                return null;
            }
            return _mapper.Map<MemberView>(Member);
        }

        public async Task<MemberView> AddMember(MemberCreationView MemberCreationView)
        {
            var MemberModel = _mapper.Map<Member>(MemberCreationView);
            var oldMember = _MemberRepository.GetMember(MemberCreationView.UserName);
            if (oldMember.Result != null)
            {
                throw new Exception("Member Already Exists");
            }
            MemberModel.MemberId = new Guid();
            var Member = await _MemberRepository.AddMember(MemberModel);
            await _MemberRepository.SaveAsync();
            return _mapper.Map<MemberView>(Member);
        }

        public async Task DeleteMember(string UserName)
        {
            _MemberRepository.DeleteMember(UserName);
            await _MemberRepository.SaveAsync();
        }

        public async Task UpdateMember(MemberCreationView MemberView)
        {
            var Member = await _MemberRepository.GetMember(MemberView.UserName);
            _mapper.Map(MemberView, Member);
            await _MemberRepository.SaveAsync();
        }
    }
}